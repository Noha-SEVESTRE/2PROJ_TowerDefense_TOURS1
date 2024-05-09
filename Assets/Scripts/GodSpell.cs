using System.Collections;
using UnityEngine;

public class GodSpell : MonoBehaviour
{
    public GameObject meteoritePrefab;
    public Transform spawnPoint;
    public int numberOfMeteorites = 15;
    public float spawnInterval = 0.5f; // Intervalle entre chaque spawn

    private bool canSpawn = true;

    private IEnumerator ResetSpawnCooldown()
    {
        yield return new WaitForSeconds(spawnInterval); // Attendre l'intervalle spécifié
        canSpawn = true;
    }

    private IEnumerator SpawnMeteoritesOneByOne()
    {
        for (int i = 0; i < numberOfMeteorites; i++)
        {
            float spawnX = Random.Range(-12f, 10f);
            float spawnY = 1f;

            Vector3 spawnPosition = new Vector3(spawnX, spawnY);

            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, -90f);

            Instantiate(meteoritePrefab, spawnPosition, spawnRotation);

            yield return new WaitForSeconds(spawnInterval); // Attendre avant de spawner la prochaine météorite
        }

        canSpawn = true; // Réinitialiser le drapeau de spawn
    }

    public void ActivateGodSpell()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnMeteoritesOneByOne()); // Lancer la coroutine de spawn
            canSpawn = false; // Désactiver le spawn jusqu'à la fin de la séquence
        }
    }
}
