using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public int baseDamage = 50; // Dégâts de base des météorites

    private int damage; // Dégâts actuels des météorites

    private void Start()
    {
        damage = baseDamage;
    }

    public int GetDamage()
    {
        return damage;
    }

    // Méthode pour mettre à jour les dégâts des météorites lors de l'activation du sort de Dieu
    public void UpdateDamage(int player1Level)
    {
        float multiplier = Mathf.Pow(1.5f, player1Level - 1);
        damage = Mathf.RoundToInt(baseDamage * multiplier);
    }

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