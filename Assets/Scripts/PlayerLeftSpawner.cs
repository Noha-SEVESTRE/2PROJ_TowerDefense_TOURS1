using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftSpawner : MonoBehaviour
{
    public GameObject melee;
    public GameObject antiarmor;
    public GameObject archer;
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
        if (canSpawn)
        {
            Instantiate(unit, spawnPoint.position, Quaternion.identity);
            canSpawn = false; // désactiver le spawn temporairement
            StartCoroutine(ResetSpawnCooldown()); // lancer le cooldown
        }
    }

    // Méthodes pour instancier chaque type d'unité
    public void SpawnMelee()
    {
        SpawnUnit(melee);
    }

    public void SpawnAntiArmor()
    {
        SpawnUnit(antiarmor);
    }

    public void SpawnArcher()
    {
        SpawnUnit(archer);
    }
}
