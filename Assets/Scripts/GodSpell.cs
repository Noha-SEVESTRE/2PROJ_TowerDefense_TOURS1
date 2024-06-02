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
    private string projectileTag;

    private bool canSpawn = true;
    public Button godSpellButton; 

    private Evolution evolutionScript;
    
    private void Start()
    {
        evolutionScript = FindObjectOfType<Evolution>();
    }

    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
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
            GameObject meteorite = Instantiate(meteoritePrefab, spawnPosition, spawnRotation);

            if (!string.IsNullOrEmpty(projectileTag))
            {
                meteorite.tag = projectileTag;

                if (evolutionScript != null && projectileTag == "Player1")
                {
                    Meteorite meteoriteComponent = meteorite.GetComponent<Meteorite>();
                    if (meteoriteComponent != null)
                    {
                        meteoriteComponent.UpdateDamage(evolutionScript.Player1Level);

                        Debug.Log("Dégâts de la météorite : " + meteoriteComponent.GetDamage());
                    }

                    SpriteRenderer meteoriteSpriteRenderer = meteorite.GetComponent<SpriteRenderer>();
                    if (meteoriteSpriteRenderer != null)
                    {
                        meteoriteSpriteRenderer.color = evolutionScript.DeterminePlayerColor(evolutionScript.Player1Level, evolutionScript.Player1Level);
                    }
                    else
                    {
                        Debug.LogWarning("Le SpriteRenderer n'a pas été trouvé sur la météorite instanciée.");
                    }
                }
                else if (evolutionScript != null && projectileTag == "Player2")
                {
                    SpriteRenderer meteoriteSpriteRenderer = meteorite.GetComponent<SpriteRenderer>();
                    if (meteoriteSpriteRenderer != null)
                    {
                        meteoriteSpriteRenderer.color = evolutionScript.DeterminePlayerColor(evolutionScript.Player2Level, evolutionScript.Player2Level);
                    }
                    else
                    {
                        Debug.LogWarning("Le SpriteRenderer n'a pas été trouvé sur la météorite instanciée.");
                    }
                }
                else
                {
                    Debug.LogWarning("Le script Evolution n'a pas été trouvé.");
                }
            }
            else
            {
                Debug.LogError("Projectile tag is null or empty!");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }



    public void ActivateGodSpell(bool isPlayerSpell)
    {
        if (isPlayerSpell)
        {
            if (PlayerStats.exp > cost)
            {
                if (canSpawn)
                {
                    PlayerStats.exp -= cost;
                    projectileTag = "Player1"; 
                    StartCoroutine(SpawnMeteoritesOneByOne());
                    canSpawn = false;
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
        else
        {
            if (IAStats.exp > cost)
            {
                if (canSpawn)
                {
                    IAStats.exp -= cost;
                    projectileTag = "Player2"; 
                    StartCoroutine(SpawnMeteoritesOneByOne());
                    canSpawn = false;
                    StartCoroutine(ResetSpawnCooldown());
                }
            }
        }
    }
}