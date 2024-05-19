using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, IDamageable
{
    public int damage = 20;
    public int maxHealth = 400;
    public int MaxHealth => maxHealth;
    public Rigidbody2D TankRb;
    public float speed;

    public int cost = 400;
    public float damageInterval = 1.5f;

    private Coroutine damageCoroutine;
    private GameObject currentTarget;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si aucun dommage n'est actuellement infligé, la collision est avec une nouvelle cible et les tags sont différents, démarrer la coroutine
        if (damageCoroutine == null && collision.gameObject != currentTarget && collision.gameObject.CompareTag(gameObject.tag) != true)
        {
            StartDamaging(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si la collision persiste avec une cible différente, changer de cible
        if (collision.gameObject != currentTarget && collision.gameObject.CompareTag(gameObject.tag) != true)
        {
            StopDamaging();
            StartDamaging(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si la cible actuelle sort de la collision, arrêter de causer des dégâts
        if (collision.gameObject == currentTarget)
        {
            StopDamaging();
        }
    }

    private void StartDamaging(GameObject target)
    {
        currentTarget = target;
        damageCoroutine = StartCoroutine(DealDamagePeriodically(currentTarget));
    }

    private void StopDamaging()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator DealDamagePeriodically(GameObject target)
    {
        while (true) // Boucle infinie pour infliger des dégâts périodiquement
        {
            HealthBar healthBar = target.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(damage);
            }

            // Attendre jusqu'à ce que le temps de dégâts soit écoulé
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Update is called 50x per second
    void FixedUpdate()
    {
        // Ajuster la direction et l'échelle en fonction du tag
        if (CompareTag("Player1"))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            TankRb.velocity = Vector2.right * speed;
        }
        else if (CompareTag("Player2"))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            TankRb.velocity = Vector2.left * speed;
        }
    }
}
