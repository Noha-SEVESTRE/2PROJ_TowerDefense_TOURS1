using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preMeleeMovement : MonoBehaviour
{
    public Rigidbody2D preMeleeRb;
    public float speed;

    // Update is called 50x per second
    void FixedUpdate()
    {
        preMeleeRb.velocity = Vector2.right * speed;
    }
}
