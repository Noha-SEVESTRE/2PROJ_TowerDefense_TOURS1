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
    spawnedUnit.tag = spawnPoint.tag; // Utiliser le tag du spawn point

    // Trouver le SpriteRenderer dans le prefab de l'unité
    SpriteRenderer unitSpriteRenderer = spawnedUnit.GetComponentInChildren<SpriteRenderer>();
    if (unitSpriteRenderer != null)
    {
        // Déterminer la couleur en fonction du niveau du joueur 1
        Evolution evolutionScript = FindObjectOfType<Evolution>();
        if (evolutionScript != null)
        {
            unitSpriteRenderer.color = evolutionScript.DeterminePlayerColor(evolutionScript.Player1Level, evolutionScript.Player1Level);
        }
        else
        {
            Debug.LogWarning("Le script Evolution n'a pas été trouvé.");
        }
    }
    else
    {
        Debug.LogWarning("Le SpriteRenderer n'a pas été trouvé dans le prefab de l'unité.");
    }

    // Upgrader les statistiques de l'unité en fonction du niveau du joueur 1 et du type d'unité
    UpgradeStats(spawnedUnit);

    // Debug.Log des statistiques maxHealth et damage de l'unité qui vient d'être spawnée
    Debug.Log("Stats de l'unité spawnée - Max Health: " + spawnedUnit.GetComponent<IDamageable>().MaxHealth);

    canSpawn = false;
    meleeButton.interactable = false;
    archerButton.interactable = false;
    antiArmorButton.interactable = false;
    tankButton.interactable = false;
}

    private void UpgradeStats(GameObject unit)
    {
        // Récupérer le niveau du joueur 1
        Evolution evolutionScript = FindObjectOfType<Evolution>();
        if (evolutionScript != null)
        {
            int player1Level = evolutionScript.GetPlayer1Level();

            // Augmenter maxHealth et damage en fonction du niveau du joueur 1 et du type d'unité
            if (unit.GetComponent<Melee>() != null)
            {
                UpgradeMeleeStats(unit.GetComponent<Melee>(), player1Level);
            }
            else if (unit.GetComponent<Archer>() != null)
            {
                UpgradeArcherStats(unit.GetComponent<Archer>(), player1Level);
            }
            else if (unit.GetComponent<AntiArmor>() != null)
            {
                UpgradeAntiArmorStats(unit.GetComponent<AntiArmor>(), player1Level);
            }
            else if (unit.GetComponent<Tank>() != null)
            {
                UpgradeTankStats(unit.GetComponent<Tank>(), player1Level);
            }
        }
        else
        {
            Debug.LogWarning("Le script Evolution n'a pas été trouvé.");
        }
    }

    private void UpgradeMeleeStats(Melee melee, int player1Level)
    {
        // Upgrader les statistiques de l'unité Melee en fonction du niveau du joueur 1
        if (player1Level >= 2)
        {
            float multiplier = Mathf.Pow(1.5f, player1Level - 1); // Calculer le multiplicateur en fonction du niveau du joueur
            melee.maxHealth = Mathf.RoundToInt(melee.maxHealth * multiplier * 10) / 10f; // Arrondir au dixième près
            melee.damage = Mathf.RoundToInt(melee.damage * multiplier * 10) / 10f; // Arrondir au dixième près
        }
    }

    private void UpgradeArcherStats(Archer archer, int player1Level)
    {
        // Upgrader les statistiques de l'unité Archer en fonction du niveau du joueur 1
        if (player1Level >= 2)
        {
            float multiplier = Mathf.Pow(1.5f, player1Level - 1); // Calculer le multiplicateur en fonction du niveau du joueur
            archer.maxHealth = Mathf.RoundToInt(archer.maxHealth * multiplier * 10) / 10f; // Arrondir au dixième près
            archer.damage = Mathf.RoundToInt(archer.damage * multiplier * 10) / 10f; // Arrondir au dixième près
        }
    }

    private void UpgradeAntiArmorStats(AntiArmor antiArmor, int player1Level)
    {
        // Upgrader les statistiques de l'unité AntiArmor en fonction du niveau du joueur 1
        if (player1Level >= 2)
        {
            float multiplier = Mathf.Pow(1.5f, player1Level - 1); // Calculer le multiplicateur en fonction du niveau du joueur
            antiArmor.maxHealth = Mathf.RoundToInt(antiArmor.maxHealth * multiplier * 10) / 10f; // Arrondir au dixième près
            antiArmor.damage = Mathf.RoundToInt(antiArmor.damage * multiplier * 10) / 10f; // Arrondir au dixième près
        }
    }

    private void UpgradeTankStats(Tank tank, int player1Level)
    {
        // Upgrader les statistiques de l'unité Tank en fonction du niveau du joueur 1
        if (player1Level >= 2)
        {
            float multiplier = Mathf.Pow(1.5f, player1Level - 1); // Calculer le multiplicateur en fonction du niveau du joueur
            tank.maxHealth = Mathf.RoundToInt(tank.maxHealth * multiplier * 10) / 10f; // Arrondir au dixième près
            tank.damage = Mathf.RoundToInt(tank.damage * multiplier * 10) / 10f; // Arrondir au dixième près
        }
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