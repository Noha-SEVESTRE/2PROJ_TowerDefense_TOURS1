using System.Collections;
using UnityEngine;

public class GodSpell : MonoBehaviour
{
    public GameObject meteoritePrefab;
    public Transform spawnPoint;
    public int numberOfMeteorites = 15;
    public float spawnInterval = 0.5f; 

    private bool canSpawn = true;

    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnInterval); 
        canSpawn = true;
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

        canSpawn = true; 
    }

    public void ActivateGodSpell()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnMeteoritesOneByOne()); 
            canSpawn = false; 
        }
    }
}
