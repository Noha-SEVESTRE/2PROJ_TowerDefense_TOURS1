using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 10f;
    public GameObject partToRotate;

    private bool detected = false;
    private Vector3 nearestEnemyPosition;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackSpeed;

    public int minDamage; // Dégât minimum de la tourelle
    public int maxDamage; // Dégât maximum de la tourelle

    public int price;

    private int sellingPrice;
    private float timeSinceLastAttack = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        sellingPrice = price / 2;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player2");
        float shortestDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
            if (enemyCollider == null)
            {
                continue;
            }

            Vector3 targetPosition = enemyCollider.bounds.center;
            float distanceToEnemy = Vector3.Distance(playerPosition, targetPosition);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemyPosition = targetPosition;
                detected = true;
            }
        }

        if (shortestDistance > range)
        {
            detected = false;
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

            timeSinceLastAttack += Time.deltaTime;

            if (timeSinceLastAttack >= 1f / attackSpeed)
            {
                Shoot();
                timeSinceLastAttack = 0f;
            }
        }
    }

    void Shoot()
    {
        int damage = Random.Range(minDamage, maxDamage + 1);
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public int GetSellingPrice()
    {
        return sellingPrice;
    }
}
