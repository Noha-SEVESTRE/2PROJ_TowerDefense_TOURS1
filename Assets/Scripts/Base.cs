using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IDamageable
{
    public float maxHealth = 500;
    public float MaxHealth => maxHealth;

    public GameObject gameOverPanel;

    public DifficultyManager difficultyManager;

    private void OnDestroy()
    {
        OnObjectDestroyed();
    }

    private void OnObjectDestroyed()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (difficultyManager != null)
        {
            difficultyManager.DeactivateActiveAI();
        }
    }
}
