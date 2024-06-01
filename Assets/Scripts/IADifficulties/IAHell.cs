using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAHell : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public GameObject tankPrefab;
    public Transform spawnPoint;

    public GameObject[] turretPositions;
    public int[] turretPrices;

    public GameObject player2TurretPositions;

    public GameObject[] availableTurrets;

    private bool canSpawn = true;
    private float spawnInterval = 3.0f;
    private float nextSpawnTime;

    private float turretCheckInterval = 20.0f;  
    private float nextTurretCheckTime;

    private float reservePercentage = 0.25f; 

    private Queue<GameObject> unitSequence;
    private int sequenceIndex = 0;

    private int currentTurretPositionIndex = 0;

    public GodSpell godSpell;
    private float godSpellCheckInterval = 90.0f;  
    private float nextGodSpellCheckTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        nextTurretCheckTime = Time.time + turretCheckInterval;
        nextGodSpellCheckTime = Time.time + godSpellCheckInterval;
        InitializeUnitSequence();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            if (canSpawn)
            {
                TrySpawnUnit();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        if (Time.time >= nextTurretCheckTime)
        {
            TryManageTurrets();
            nextTurretCheckTime = Time.time + turretCheckInterval;
        }

        if (Time.time >= nextGodSpellCheckTime)
        {
            TryUseGodSpell();
            nextGodSpellCheckTime = Time.time + godSpellCheckInterval;
        }
    }

    private void InitializeUnitSequence()
    {
        unitSequence = new Queue<GameObject>();
        unitSequence.Enqueue(tankPrefab);
        unitSequence.Enqueue(archerPrefab);
        unitSequence.Enqueue(meleePrefab);
        unitSequence.Enqueue(archerPrefab);
        unitSequence.Enqueue(antiArmorPrefab);
        unitSequence.Enqueue(meleePrefab);
        unitSequence.Enqueue(antiArmorPrefab);
        unitSequence.Enqueue(archerPrefab);
        unitSequence.Enqueue(tankPrefab);
        unitSequence.Enqueue(antiArmorPrefab);
    }

    private void TrySpawnUnit()
    {
        if (!canSpawn)
            return;

        if (CountIAUnits() >= 10)
        {
            Debug.Log("Nombre maximum d'unités atteint. L'IA ne peut pas faire spawn plus d'unités.");
            return;
        }

        GameObject unitToSpawn = ChooseNextUnitInSequence();

        if (unitToSpawn == null)
            return;

        int unitCost = GetUnitCost(unitToSpawn);

        if (!IAStats.CanAfford(unitCost) && !IsCriticalSituation())
        {
            Debug.Log("IA n'a pas assez d'argent pour cette unité.");
            return;
        }

        IAStats.SpendMoney(unitCost);
        Debug.Log($"Gold de l'IA après spawn de l'unité: {IAStats.money}");
        SpawnUnit(unitToSpawn);
        sequenceIndex = (sequenceIndex + 1) % unitSequence.Count;
    }

    private GameObject ChooseNextUnitInSequence()
    {
        int totalResources = IAStats.money;
        int reservedResources = Mathf.FloorToInt(totalResources * reservePercentage);

        GameObject nextUnit = null;

        for (int i = 0; i < unitSequence.Count; i++)
        {
            nextUnit = unitSequence.ToArray()[(sequenceIndex + i) % unitSequence.Count];
            if (IAStats.CanAfford(GetUnitCost(nextUnit)) && totalResources - GetUnitCost(nextUnit) >= reservedResources)
            {
                sequenceIndex = (sequenceIndex + i) % unitSequence.Count;
                return nextUnit;
            }
        }

        if (IsCriticalSituation())
        {
            for (int i = 0; i < unitSequence.Count; i++)
            {
                nextUnit = unitSequence.ToArray()[(sequenceIndex + i) % unitSequence.Count];
                if (IAStats.CanAfford(GetUnitCost(nextUnit)))
                {
                    sequenceIndex = (sequenceIndex + i) % unitSequence.Count;
                    return nextUnit;
                }
            }
        }

        Debug.Log("IA ne peut se permettre aucune unité en respectant la réserve.");
        return null;
    }

    private int GetUnitCost(GameObject unit)
    {
        if (unit == meleePrefab)
            return meleePrefab.GetComponent<Melee>().cost;
        else if (unit == antiArmorPrefab)
            return antiArmorPrefab.GetComponent<AntiArmor>().cost;
        else if (unit == archerPrefab)
            return archerPrefab.GetComponent<Archer>().cost;
        else if (unit == tankPrefab)
            return tankPrefab.GetComponent<Tank>().cost;

        return 0;
    }

    private void SpawnUnit(GameObject unit)
    {
        GameObject spawnedUnit = Instantiate(unit, spawnPoint.position, Quaternion.identity);
        spawnedUnit.tag = spawnPoint.tag;
        canSpawn = false;
        StartCoroutine(ResetSpawnCooldown());
    }

    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }

    private void TryManageTurrets()
    {
        if (currentTurretPositionIndex >= turretPositions.Length)
            return;

        int positionCost = turretPrices[currentTurretPositionIndex];

        if (IAStats.CanAfford(positionCost))
        {
            IAStats.SpendMoney(positionCost);
            turretPositions[currentTurretPositionIndex].SetActive(true);
            Debug.Log($"IA a acheté l'emplacement de tourelle {currentTurretPositionIndex} pour {positionCost} gold.");
            currentTurretPositionIndex++;
            nextTurretCheckTime = Time.time + turretCheckInterval; 

            TryBuildTurret();
        }
        else
        {
            Debug.Log($"IA ne peut pas acheter l'emplacement de tourelle {currentTurretPositionIndex} - pas assez de gold.");
        }
    }

    private void TryBuildTurret()
    {
        if (currentTurretPositionIndex == 0 || turretPositions.Length == 0)
            return;

        GameObject turretPrefab = ChooseTurretToBuild();

        if (turretPrefab == null)
            return;

        for (int i = 0; i < currentTurretPositionIndex; i++)
        {
            if (turretPositions[i].transform.childCount == 0)
            {
                GameObject turret = Instantiate(turretPrefab, turretPositions[i].transform.position, Quaternion.identity);
                turret.tag = "Player2";
                turret.transform.SetParent(turretPositions[i].transform);
                Debug.Log($"IA a placé une tourelle sur l'emplacement {i}.");
                player2TurretPositions.SetActive(true);
                return;
            }
        }
    }

    private GameObject ChooseTurretToBuild()
    {
        GameObject bestTurret = null;
        int highestPrice = 0;

        foreach (GameObject turret in availableTurrets)
        {
            if (turret == null)
            {
                Debug.LogWarning("Une des tourelles disponibles est null.");
                continue;
            }

            int turretCost = turret.GetComponent<TurretIA>().price;
            Debug.Log($"Tourelle: {turret.name}, Coût: {turretCost}, Argent IA: {IAStats.money}");

            if (turretCost > highestPrice && IAStats.CanAfford(turretCost))
            {
                bestTurret = turret;
                highestPrice = turretCost;
            }
        }

        return bestTurret;
    }

    private bool IsCriticalSituation()
    {
        int playerTurretCount = GameObject.FindGameObjectsWithTag("Player").Length;
        int iaTurretCount = GameObject.FindGameObjectsWithTag("Player2").Length;

        if (playerTurretCount > iaTurretCount + 2 || IAStats.money < 20)
            return true;

        return false;
    }

    private int CountIAUnits()
    {
        GameObject[] iaUnits = GameObject.FindGameObjectsWithTag(spawnPoint.tag);
        return iaUnits.Length;
    }

    private int CountPlayerUnits()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player1");
        return playerUnits.Length;
    }

    private void TryUseGodSpell()
    {
        if (IAStats.CanAfford(godSpell.cost) && IAStats.exp > 2500 && CountPlayerUnits() >= 7)
        {
            godSpell.ActivateGodSpell(false);
            Debug.Log("L'IA a utilisé un GodSpell.");
        }
        else
        {
            if (IAStats.exp <= 2500)
            {
                Debug.Log("L'IA n'utilise pas le GodSpell car son expérience est insuffisante.");
            }
            else if (CountPlayerUnits() < 7)
            {
                Debug.Log("L'IA n'utilise pas le GodSpell car il y a moins de 7 unités de 'Player1' sur le terrain.");
            }
            else
            {
                Debug.Log("L'IA ne peut pas utiliser le GodSpell car elle ne peut pas se le permettre.");
            }
        }
    }
}