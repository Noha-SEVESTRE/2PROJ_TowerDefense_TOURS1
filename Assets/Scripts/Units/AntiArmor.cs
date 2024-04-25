using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiArmor : MonoBehaviour
{
    public Rigidbody2D AntiArmorRb;
    public float speed;

    // Update is called 50x per second
    void FixedUpdate()
    {
        AntiArmorRb.velocity = Vector2.right * speed;
    }
}
