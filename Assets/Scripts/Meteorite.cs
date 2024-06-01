using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {

    public int damage = 50;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Si la météorite touche le sol, elle se détruit
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(gameObject.tag))
        {
            // Si la météorite touche un objet du même tag, elle se détruit sans infliger de dégâts
            Destroy(gameObject);
        }
        else
        {
            // Si la météorite touche un objet avec un tag différent, elle inflige des dégâts à cet objet et se détruit
            HealthBar healthBar = collision.gameObject.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(damage);
            }
            Destroy(gameObject);
        }
    }
}
