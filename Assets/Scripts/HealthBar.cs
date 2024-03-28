/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    public Image Health;

    public int health;
    public int maxHealth = 100;
    public float decreaseSpeed = 50; // Vitesse de diminution de la barre de vie

    private float targetHp; // Valeur de la barre de vie � laquelle nous voulons nous rendre

    private void Start()
    {
        health = maxHealth;
        targetHp = health; // Initialement, notre objectif est la valeur actuelle
    }

    private void Update()
    {
        if (health != targetHp)
        {
            health = Mathf.Lerp(health, targetHp, Time.deltaTime * decreaseSpeed);
            UpdateUi();
        }
    }

    
    public void Attack(float damage)
    {
        targetHp = Mathf.Clamp(currentHp - damage, minHp, maxHp);
    }

    public void Heal(float healAmount)
    {
        targetHp = Mathf.Clamp(currentHp + healAmount, minHp, maxHp);
    }
    

    void UpdateUi()
    {
        Health.fillAmount = health / maxHealth;
        Health.color = gradient.Evaluate(Health.fillAmount);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    public Image Health;

    public int maxHealth = 100;
    private float currentHealth; // Utilisation de float pour une transition en douceur

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUi();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Méthode pour mettre à jour la barre de vie
    public void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Garantit que la vie ne dépasse pas les limites
        UpdateUi();
    }

    // Méthode pour mettre à jour l'apparence de la barre de vie
    void UpdateUi()
    {
        float fillAmount = currentHealth / maxHealth;
        Health.fillAmount = fillAmount;
        Health.color = gradient.Evaluate(fillAmount);
    }
}