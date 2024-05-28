using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GodSpell : MonoBehaviour
{
    public GameObject meteoritePrefab;
    public Transform spawnPoint;
    public int numberOfMeteorites = 15;
    public float spawnInterval = 0.5f;
    public int spawnCooldown = 40;
    public int cost = 2000;

    private bool canSpawn = true;
    public Button godSpellButton; // Référence au bouton associé à la fonction ActivateGodSpell
    
    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
        // Réactivez le bouton après le cooldown
        godSpellButton.interactable = true;
    }

    private IEnumerator SpawnMeteoritesOneByOne()
    {
        for (int i = 0; i < numberOfMeteorites; i++)
        {
            float spawnX = Random.Range(-12f, 10f);
            float spawnY = 2f;
            Vector3 spawnPosition = new Vector3(spawnX, spawnY);
            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, -90f);
            Instantiate(meteoritePrefab, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void ActivateGodSpell()
    {
        if (PlayerStats.exp > cost)
        {
            if (canSpawn)
            {
                PlayerStats.exp -= cost;
                StartCoroutine(SpawnMeteoritesOneByOne());
                canSpawn = false;
                // Désactivez le bouton pendant le cooldown
                godSpellButton.interactable = false;
                StartCoroutine(ResetSpawnCooldown());
            }
            else
            {
                Debug.Log("Cooldown not finished");
            }
        }
        else
        {
            Debug.Log("Not enough exp");
        }
    }
}
