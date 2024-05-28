using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeftSpawner : MonoBehaviour
{
    public GameObject meleePrefab; // Référence au prefab de l'unité melee
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public GameObject tankPrefab;
    public Transform spawnPoint;

    public Button meleeButton;
    public Button archerButton;
    public Button antiArmorButton;
    public Button tankButton;

    private bool canSpawn = true; // variable pour vérifier si le spawn est possible

    // Fonction pour désactiver le cooldown après un certain temps
    private IEnumerator ResetSpawnCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
        meleeButton.interactable = true;
        archerButton.interactable = true;
        antiArmorButton.interactable = true;
        tankButton.interactable = true;
    }

    // Fonction pour instancier une unité avec un cooldown
    private void SpawnUnit(GameObject unit)
    {
        GameObject spawnedUnit = Instantiate(unit, spawnPoint.position, Quaternion.identity);
        spawnedUnit.tag = spawnPoint.tag; // Assigner le tag du spawnPoint à l'unité générée
        canSpawn = false; // désactiver le spawn temporairement
        meleeButton.interactable = false;
        archerButton.interactable = false;
        antiArmorButton.interactable = false;
        tankButton.interactable = false;
    }

    // Méthodes pour instancier chaque type d'unité
    public void SpawnMelee()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Melee meleeScript = meleePrefab.GetComponent<Melee>(); // Obtenir la référence à la classe Melee
        Debug.Log("Money before spawning: " + PlayerStats.money);

        if (PlayerStats.money < meleeScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= meleeScript.cost;
        SpawnUnit(meleePrefab);
        StartCoroutine(ResetSpawnCooldown(meleeScript.cooldown)); // lancer le cooldown
    }


    public void SpawnAntiArmor()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        AntiArmor antiArmorScript = antiArmorPrefab.GetComponent<AntiArmor>(); // Obtenir la référence à la classe AntiArmor
        Debug.Log("Money before spawning: " + PlayerStats.money);
        
        if (PlayerStats.money < antiArmorScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= antiArmorScript.cost;
        SpawnUnit(antiArmorPrefab);
        StartCoroutine(ResetSpawnCooldown(antiArmorScript.cooldown)); // lancer le cooldown
    }

    public void SpawnArcher()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Archer archerScript = archerPrefab.GetComponent<Archer>(); // Obtenir la référence à la classe Archer
        Debug.Log("Money before spawning: " + PlayerStats.money);
        
        if (PlayerStats.money < archerScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= archerScript.cost;
        SpawnUnit(archerPrefab);
        StartCoroutine(ResetSpawnCooldown(archerScript.cooldown)); // lancer le cooldown
    }

    public void SpawnTank()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Tank tankScript = tankPrefab.GetComponent<Tank>(); // Obtenir la référence à la classe Tank
        Debug.Log("Money before spawning: " + PlayerStats.money);
        
        if (PlayerStats.money < tankScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= tankScript.cost;
        SpawnUnit(tankPrefab);
        StartCoroutine(ResetSpawnCooldown(tankScript.cooldown)); // lancer le cooldown
    }
}