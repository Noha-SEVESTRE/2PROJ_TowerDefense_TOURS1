using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Rigidbody2D ArcherRb;
    public float speed;

    // Update is called 50x per second
    void FixedUpdate()
    {
        ArcherRb.velocity = Vector2.right * speed;
    }
}