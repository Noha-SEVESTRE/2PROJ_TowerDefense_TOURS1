using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IDamageable
{
    public int maxHealth = 500;
    public int MaxHealth => maxHealth;

    // Référence au GameOverPanel
    public GameObject gameOverPanel;

    // Cette fonction est appelée lorsque le GameObject est détruit
    private void OnDestroy()
    {
        OnObjectDestroyed();
    }

    // Cette fonction contient la logique à exécuter lors de la destruction
    private void OnObjectDestroyed()
    {
        // Affiche le GameOverPanel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Time.timeScale à la fin : " + Time.timeScale);
        }
    }
}
