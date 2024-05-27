using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 15f;
    private int damage;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(damage);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player1"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }
}
