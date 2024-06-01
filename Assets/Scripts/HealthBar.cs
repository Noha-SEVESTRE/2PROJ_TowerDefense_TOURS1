using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    public Image Health;
    private float currentHealth; 

    private void Start()
    {
        IDamageable damageable = GetComponent<IDamageable>();
        if (damageable != null)
        {
            currentHealth = damageable.MaxHealth;
            UpdateUi();
        }
    }

    public void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, GetComponent<IDamageable>().MaxHealth); 
        
        UpdateUi();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateUi()
    {
        float fillAmount = currentHealth / GetComponent<IDamageable>().MaxHealth;
        Health.fillAmount = fillAmount;
        Health.color = gradient.Evaluate(fillAmount);
    }
}
