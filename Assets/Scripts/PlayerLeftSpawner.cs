using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeftSpawner : MonoBehaviour
{
    public GameObject meleePrefab;
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public GameObject tankPrefab;
    public Transform spawnPoint;

    public Button meleeButton;
    public Button archerButton;
    public Button antiArmorButton;
    public Button tankButton;

    private bool canSpawn = true;

    private int CountPlayer1Units()
    {
        int count = 0;
        GameObject[] player1Units = GameObject.FindGameObjectsWithTag("Player1");
        foreach (GameObject unit in player1Units)
        {
            if (unit.GetComponent<Melee>() || unit.GetComponent<Archer>() || unit.GetComponent<AntiArmor>() || unit.GetComponent<Tank>())
            {
                count++;
            }
        }
        return count;
    }

    private bool CanSpawnUnit()
    {
        return CountPlayer1Units() < 10;
    }

    private IEnumerator ResetSpawnCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
        meleeButton.interactable = true;
        archerButton.interactable = true;
        antiArmorButton.interactable = true;
        tankButton.interactable = true;
    }

    private void SpawnUnit(GameObject unit)
    {
        if (!CanSpawnUnit())
        {
            Debug.Log("Impossible de faire spawn une unité, le nombre maximum d'unités sur le terrain est atteint.");
            return;
        }

        GameObject spawnedUnit = Instantiate(unit, spawnPoint.position, Quaternion.identity);
        spawnedUnit.tag = spawnPoint.tag;
        canSpawn = false;
        meleeButton.interactable = false;
        archerButton.interactable = false;
        antiArmorButton.interactable = false;
        tankButton.interactable = false;
    }

    public void SpawnMelee()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Melee meleeScript = meleePrefab.GetComponent<Melee>();
        Debug.Log("Money before spawning: " + PlayerStats.money);

        if (PlayerStats.money < meleeScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= meleeScript.cost;
        SpawnUnit(meleePrefab);
        StartCoroutine(ResetSpawnCooldown(meleeScript.cooldown));
    }

    public void SpawnAntiArmor()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        AntiArmor antiArmorScript = antiArmorPrefab.GetComponent<AntiArmor>();
        Debug.Log("Money before spawning: " + PlayerStats.money);

        if (PlayerStats.money < antiArmorScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= antiArmorScript.cost;
        SpawnUnit(antiArmorPrefab);
        StartCoroutine(ResetSpawnCooldown(antiArmorScript.cooldown));
    }

    public void SpawnArcher()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Archer archerScript = archerPrefab.GetComponent<Archer>();
        Debug.Log("Money before spawning: " + PlayerStats.money);

        if (PlayerStats.money < archerScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= archerScript.cost;
        SpawnUnit(archerPrefab);
        StartCoroutine(ResetSpawnCooldown(archerScript.cooldown));
    }

    public void SpawnTank()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Tank tankScript = tankPrefab.GetComponent<Tank>();
        Debug.Log("Money before spawning: " + PlayerStats.money);

        if (PlayerStats.money < tankScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= tankScript.cost;
        SpawnUnit(tankPrefab);
        StartCoroutine(ResetSpawnCooldown(tankScript.cooldown));
    }
}
