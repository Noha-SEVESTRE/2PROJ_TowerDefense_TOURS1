using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public Rigidbody2D ArrowRb;
    public float speed;

    public string shooterTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(shooterTag))
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

    void FixedUpdate()
    {
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
