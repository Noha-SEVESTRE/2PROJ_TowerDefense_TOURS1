using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public int baseDamage = 50;

    private int damage;

    private void Start()
    {
        damage = baseDamage;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void UpdateDamage(int player1Level)
    {
        float multiplier = Mathf.Pow(1.5f, player1Level - 1);
        damage = Mathf.RoundToInt(baseDamage * multiplier);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(gameObject.tag))
        {
            Destroy(gameObject);
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
}