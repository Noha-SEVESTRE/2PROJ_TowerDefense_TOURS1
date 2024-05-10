using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftSpawner : MonoBehaviour
{
    public GameObject meleePrefab; // Référence au prefab de l'unité melee
    public GameObject antiArmorPrefab;
    public GameObject archerPrefab;
    public Transform spawnPoint;

    private bool canSpawn = true; // variable pour vérifier si le spawn est possible

    // Fonction pour désactiver le cooldown après un certain temps
    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canSpawn = true;
    }

    // Fonction pour instancier une unité avec un cooldown
    private void SpawnUnit(GameObject unit)
    {
        Instantiate(unit, spawnPoint.position, Quaternion.identity);
        canSpawn = false; // désactiver le spawn temporairement
        StartCoroutine(ResetSpawnCooldown()); // lancer le cooldown
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
    }


    public void SpawnAntiArmor()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        AntiArmor antiArmorScript = antiArmorPrefab.GetComponent<AntiArmor>(); // Obtenir la référence à la classe Melee
        Debug.Log("Money before spawning: " + PlayerStats.money);
        
        if (PlayerStats.money < antiArmorScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= antiArmorScript.cost;
        SpawnUnit(antiArmorPrefab);
    }

    public void SpawnArcher()
    {
        if (!canSpawn)
        {
            Debug.Log("Cooldown pas terminé");
            return;
        }

        Archer archerScript = archerPrefab.GetComponent<Archer>(); // Obtenir la référence à la classe Melee
        Debug.Log("Money before spawning: " + PlayerStats.money);
        
        if (PlayerStats.money < archerScript.cost)
        {
            Debug.Log("Pas assez d'argent pour cette unité");
            return;
        }

        PlayerStats.money -= archerScript.cost;
        SpawnUnit(archerPrefab);
    }
}
