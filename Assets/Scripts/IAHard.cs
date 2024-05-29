using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAHard : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public GameObject tankPrefab;
    public Transform spawnPoint;

    public GameObject[] turretPositions;  // Références aux positions des tourelles IA
    public int[] turretPrices;  // Prix des emplacements de tourelles

    public GameObject player2TurretPositions; // Référence au GameObject Player2TurretPositions

    public GameObject[] availableTurrets; // Liste des tourelles disponibles

    private bool canSpawn = true;
    private float spawnInterval = 3.0f;
    private float nextSpawnTime;

    private float turretCheckInterval = 10.0f; // Intervalle de vérification des emplacements de tourelles
    private float nextTurretCheckTime;

    private float reservePercentage = 0.2f; // Pourcentage des ressources à conserver

    private Queue<GameObject> unitSequence;
    private int sequenceIndex = 0;

    private int currentTurretPositionIndex = 0;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        nextTurretCheckTime = Time.time + turretCheckInterval; // Initialiser le temps de vérification des tourelles
        //InitializeUnitSequence();
    }

    private void Update()
    {
        /*if (Time.time >= nextSpawnTime)
        {
            if (canSpawn)
            {
                TrySpawnUnit();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }*/

        if (Time.time >= nextTurretCheckTime)
        {
            if (TryBuyTurretPosition())
            {
                TryBuildTurret();
            }
            nextTurretCheckTime = Time.time + turretCheckInterval; // Réinitialiser le temps de vérification des tourelles
        }
    }

    /*private void InitializeUnitSequence()
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
    }*/

    /*private void TrySpawnUnit()
    {
        if (!canSpawn)
            return;

        GameObject unitToSpawn = ChooseNextUnitInSequence();

        if (unitToSpawn == null)
            return;

        int unitCost = GetUnitCost(unitToSpawn);

        if (!IAStats.CanAfford(unitCost))
        {
            Debug.Log("IA n'a pas assez d'argent pour cette unité.");
            return;
        }

        IAStats.SpendMoney(unitCost);
        SpawnUnit(unitToSpawn);
        sequenceIndex = (sequenceIndex + 1) % unitSequence.Count; // Update sequence index
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
        spawnedUnit.tag = spawnPoint.tag; // Assigner le tag du spawnPoint à l'unité générée
        canSpawn = false;
        StartCoroutine(ResetSpawnCooldown());
    }

    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }*/

    private bool TryBuyTurretPosition()
    {
        if (currentTurretPositionIndex >= turretPositions.Length)
            return false;

        int positionCost = turretPrices[currentTurretPositionIndex];

        if (IAStats.CanAfford(positionCost))
        {
            IAStats.SpendMoney(positionCost);
            turretPositions[currentTurretPositionIndex].SetActive(true);
            Debug.Log($"IA a acheté l'emplacement de tourelle {currentTurretPositionIndex} pour {positionCost} gold.");
            currentTurretPositionIndex++;
            return true;
        }
        else
        {
            Debug.Log($"IA ne peut pas acheter l'emplacement de tourelle {currentTurretPositionIndex} - pas assez de gold.");
            return false;
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
                turret.tag = "Player2"; // Assigner le tag Player2 à la tourelle générée
                turret.transform.SetParent(turretPositions[i].transform); // Assigner le parent pour éviter de placer plusieurs tourelles sur le même emplacement

                // Flipper la tourelle sur l'axe X pour qu'elle soit tournée vers la gauche
                //FlipTurret(turret);

                Debug.Log($"IA a placé une tourelle sur l'emplacement {i}.");

                // Activer le GameObject Player2TurretPositions lorsque la tourelle est placée
                player2TurretPositions.SetActive(true);

                return; 
            }
        }
    }

    /*private void FlipTurret(GameObject turret)
    {
        SpriteRenderer spriteRenderer = turret.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            Debug.LogWarning("Le GameObject de la tourelle n'a pas de SpriteRenderer.");
        }
    }*/

    private GameObject ChooseTurretToBuild()
    {
        GameObject mostExpensiveTurret = null;
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
                highestPrice = turretCost;
                mostExpensiveTurret = turret;
            }
        }

        if (mostExpensiveTurret != null)
        {
            IAStats.SpendMoney(highestPrice);
            Debug.Log($"IA a acheté une tourelle pour {highestPrice} gold.");
            return mostExpensiveTurret;
        }
        else
        {
            Debug.Log("IA ne peut pas acheter une tourelle - pas assez de gold.");
        }

        return null;
    }
}
