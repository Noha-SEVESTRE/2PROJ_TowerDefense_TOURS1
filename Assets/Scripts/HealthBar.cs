using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    public Image Health;
    private float currentHealth; // Utilisation de float pour une transition en douceur

    private void Start()
    {
        IDamageable damageable = GetComponent<IDamageable>();
        if (damageable != null)
        {
            currentHealth = damageable.MaxHealth;
            UpdateUi();
        }
    }

    // Méthode pour mettre à jour la barre de vie
    public void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, GetComponent<IDamageable>().MaxHealth); // Garantit que la vie ne dépasse pas les limites
        
        // Met à jour l'UI après avoir ajusté la santé
        UpdateUi();

        // Vérifie si la santé est inférieure ou égale à zéro pour supprimer l'objet
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Méthode pour mettre à jour l'apparence de la barre de vie
    void UpdateUi()
    {
        float fillAmount = currentHealth / GetComponent<IDamageable>().MaxHealth;
        Health.fillAmount = fillAmount;
        Health.color = gradient.Evaluate(fillAmount);
    }
}
