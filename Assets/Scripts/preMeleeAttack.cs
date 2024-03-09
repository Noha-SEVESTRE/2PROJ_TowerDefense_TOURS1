using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preMeleeAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<towerHealth>())
        {
            collision.gameObject.GetComponent<towerHealth>().health -= damage;
            Destroy(gameObject);
        }
    }
}
