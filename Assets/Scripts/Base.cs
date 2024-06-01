using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IDamageable
{
    public int maxHealth = 500;
    public int MaxHealth => maxHealth;

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
