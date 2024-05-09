using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {

    public int damage = 50;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Meteorite")) // Access the other game object using collision.gameObject
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
