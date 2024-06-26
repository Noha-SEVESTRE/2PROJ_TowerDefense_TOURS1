using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 15f;
    private float damage;
    public string shooterTag;

    void Update()
    {
        if (shooterTag == "TurretPlayer1")
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(shooterTag))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else
        {
            if (shooterTag == "TurretPlayer1")
            {
                if (collision.gameObject.CompareTag("Player1"))
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
                }
                else
                {
                    HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
                    if (healthBar != null)
                    {
                        healthBar.UpdateHealth(damage);
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("Player2"))
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
                }
                else
                {
                    HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
                    if (healthBar != null)
                    {
                        healthBar.UpdateHealth(damage);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }
}
