using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("RestartGame called, resetting Time.timeScale to 1");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu called, resetting Time.timeScale to 1");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
