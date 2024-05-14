using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour, IDamageable
{
    public Rigidbody2D ArcherRb;
    public float speed;
    public int maxHealth = 75;
    public int MaxHealth => maxHealth;

    public int cost = 125;

    // Update is called 50x per second
    void FixedUpdate()
    {
        ArcherRb.velocity = Vector2.right * speed;
    }
}
