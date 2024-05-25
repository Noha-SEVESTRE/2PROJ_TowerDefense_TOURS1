using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 10f;
    public GameObject partToRotate;

    private bool detected = false;
    private Vector3 nearestEnemyPosition;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackSpeed = 0.75f;
    private float attackCooldown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player2");
        float shortestDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position; // Position de votre tourelle

        foreach (GameObject enemy in enemies)
        {
            BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
            if (enemyCollider == null)
            {
                continue; // Passe à l'itération suivante si l'ennemi n'a pas de BoxCollider2D
            }

            Vector3 targetPosition = enemyCollider.bounds.center;

            float distanceToEnemy = Vector3.Distance(playerPosition, targetPosition);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemyPosition = targetPosition;
                detected = true; // Marquez l'ennemi comme détecté
            }
        }

        if (shortestDistance > range)
        {
            detected = false; // Marquez l'ennemi comme non détecté
        }
    }

    void Update()
    {
        if (detected)
        {
            Vector2 direction = nearestEnemyPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            partToRotate.transform.rotation = Quaternion.Slerp(partToRotate.transform.rotation, rotation, 5f * Time.deltaTime);

            if (Time.time > attackCooldown)
            {
                attackCooldown = Time.time + 1 / attackSpeed;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}