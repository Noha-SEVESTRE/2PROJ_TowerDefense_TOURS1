using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject difficultyPanel;
    public GameObject mainMenuPanel; 

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false); 
        difficultyPanel.SetActive(true); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("GameDifficulty", difficulty); 
        SceneManager.LoadSceneAsync(1);
        Debug.Log("Time.timeScale au début : " + Time.timeScale);
    }

    public void BackToMainMenu()
    {
        difficultyPanel.SetActive(false); 
        mainMenuPanel.SetActive(true); 
    }
}
