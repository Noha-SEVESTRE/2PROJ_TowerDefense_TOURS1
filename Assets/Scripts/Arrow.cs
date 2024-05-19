using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 15;
    public Rigidbody2D ArrowRb;
    public float speed;

    // Ajout d'une variable pour stocker le tag de l'unité qui a tiré la flèche
    public string shooterTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignorer les collisions avec les unités alliées
        if (collision.gameObject.CompareTag(shooterTag))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        else
        {
            // Infliger des dégâts aux unités ennemies
            HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(damage);
            }
            Destroy(gameObject);
        }
    }

    // Update is called 50x per second
    void FixedUpdate()
    {
        // Déplacement de la flèche en fonction du tag de l'unité qui l'a tirée
        if (shooterTag == "Player1")
        {
            ArrowRb.velocity = Vector2.right * speed;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (shooterTag == "Player2")
        {
            ArrowRb.velocity = Vector2.left * speed;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
