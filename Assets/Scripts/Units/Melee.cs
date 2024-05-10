using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int damage = 25;
    public Rigidbody2D MeleeRb;
    public float speed;

    public int cost = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.UpdateHealth(damage);
        }
    }

    // Update is called 50x per second
    void FixedUpdate()
    {
        MeleeRb.velocity = Vector2.right * speed;
    }
}