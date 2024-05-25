using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEazy : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public GameObject tankPrefab;
    public Transform spawnPoint;

    private bool canSpawn = true;
    private float spawnInterval = 3.0f; //A revoir?
    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawnUnit();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void TrySpawnUnit()
    {
        if (!canSpawn)
            return;

        GameObject unitToSpawn = ChooseRandomUnit();

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
    }

    private GameObject ChooseRandomUnit()
    {
        List<GameObject> availableUnits = new List<GameObject>();

        if (IAStats.CanAfford(GetUnitCost(meleePrefab)))
            availableUnits.Add(meleePrefab);

        if (IAStats.CanAfford(GetUnitCost(antiArmorPrefab)))
            availableUnits.Add(antiArmorPrefab);

        if (IAStats.CanAfford(GetUnitCost(archerPrefab)))
            availableUnits.Add(archerPrefab);

        if (IAStats.CanAfford(GetUnitCost(tankPrefab)))
            availableUnits.Add(tankPrefab);

        if (availableUnits.Count == 0)
        {
            Debug.Log("IA ne peut se permettre aucune unité.");
            return null;
        }

        return availableUnits[Random.Range(0, availableUnits.Count)];
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
        yield return new WaitForSeconds(1.5f);
        canSpawn = true;
    }
}
