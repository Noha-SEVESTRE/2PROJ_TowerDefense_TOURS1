using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public int damage = 25;
    private float timer;

    void Update()
    {
        // Déplacer le projectile dans la direction actuelle à la vitesse donnée
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        timer += Time.deltaTime;
        
        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si la collision est avec un objet tagué "Player2"
        if (collision.gameObject.CompareTag("Player2"))
        {
            // Si la collision est avec un objet de tag "Player2", appliquer des dégâts et détruire le projectile
            HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(damage);
            }
            Destroy(gameObject);
        }
        // Sinon, si la collision est avec un objet tagué "Player1", ignorer la collision
        else if (collision.gameObject.CompareTag("Player1"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}