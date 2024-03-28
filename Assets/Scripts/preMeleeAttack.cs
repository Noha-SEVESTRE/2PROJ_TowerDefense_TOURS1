/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preMeleeAttack : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<HealthBar>())
        {
            collision.gameObject.GetComponent<HealthBar>().health -= damage;
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preMeleeAttack : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.UpdateHealth(damage);
        }
    }
}