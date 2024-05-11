using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Rigidbody2D TankRb;
    public float speed;

    public int cost = 400;

    // Update is called 50x per second
    void FixedUpdate()
    {
        TankRb.velocity = Vector2.right * speed;
    }
}
