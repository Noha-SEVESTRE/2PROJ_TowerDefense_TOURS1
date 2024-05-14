using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 15;
    public Rigidbody2D ArrowRb;
    public float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        else {
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
        ArrowRb.velocity = Vector2.right * speed;
    }
}
