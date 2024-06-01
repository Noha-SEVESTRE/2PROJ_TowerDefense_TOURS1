using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANormal : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public GameObject tankPrefab;
    public Transform spawnPoint;

    private bool canSpawn = true;
    private float spawnInterval = 3.0f;
    private float nextSpawnTime;

    private float reservePercentage = 0.2f; 

    private Queue<GameObject> unitSequence;
    private int sequenceIndex = 0;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
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

        if (!IAStats.CanAfford(unitCost))
        {
            Debug.Log("IA n'a pas assez d'argent pour cette unité.");
            return;
        }

        IAStats.SpendMoney(unitCost);
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

    private int CountIAUnits()
    {
        int count = 0;
        GameObject[] IAUnits = GameObject.FindGameObjectsWithTag("Player2");
        foreach (GameObject unit in IAUnits)
        {
            if (unit.GetComponent<Melee>() || unit.GetComponent<Archer>() || unit.GetComponent<AntiArmor>() || unit.GetComponent<Tank>())
            {
                count++;
            }
        }
        return count;
    }
}
