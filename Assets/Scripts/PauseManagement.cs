using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManagement : MonoBehaviour
{
    public GameObject pausePanel; // Référence au panel de pause

    private bool isPaused = false;
    private float savedTimeScale; // Pour sauvegarder la vitesse du jeu avant la pause

    void Start()
    {
        pausePanel.SetActive(false); // Assurez-vous que le panel de pause est désactivé au démarrage
    }

    // Fonction appelée lors du clic sur le bouton Pause
    public void TogglePause()
    {
        isPaused = !isPaused; // Inverse l'état de pause
        if (isPaused)
        {
            savedTimeScale = Time.timeScale; // Sauvegarde la vitesse du jeu avant la pause
            Time.timeScale = 0f; // Freeze la scène
            pausePanel.SetActive(true); // Affiche le panel de pause
        }
        else
        {
            Time.timeScale = savedTimeScale; // Restaure la vitesse du jeu avant la pause
            pausePanel.SetActive(false); // Cache le panel de pause
        }
    }

    // Fonction appelée lors du clic sur le bouton Resume
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = savedTimeScale; // Restaure la vitesse du jeu avant la pause
        pausePanel.SetActive(false); // Cache le panel de pause
    }

    // Fonction appelée lors du clic sur le bouton Restart
    public void RestartGame()
    {
        Time.timeScale = 1f; // Assurez-vous que le temps est dégelé
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recharge la scène actuelle pour recommencer à zéro
    }

    // Fonction appelée lors du clic sur le bouton MainMenu
    public void MainMenu()
    {
        Time.timeScale = 1f; // Assurez-vous que le temps est dégelé
        SceneManager.LoadScene(0); // Charge la scène 0 pour le menu principal
    }

    // Fonction appelée lors du clic sur le bouton Quit
    public void QuitGame()
    {
        Application.Quit(); // Ferme le jeu
    }
}
