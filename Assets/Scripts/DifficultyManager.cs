using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public GameObject IAEasyManagement;
    public GameObject IANormalManagement;
    public GameObject IAHardManagement;
    public GameObject IAHellManagement;

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("GameDifficulty");

        switch(difficulty)
        {
            case "Easy":
                IAEasyManagement.SetActive(true);
                break;
            case "Normal":
                IANormalManagement.SetActive(true);
                break;
            case "Hard":
                IAHardManagement.SetActive(true);
                break;
            case "Hell":
                IAHellManagement.SetActive(true);
                break;
            default:
                Debug.LogError("Difficulté inconnue: " + difficulty);
                break;
        }
    }
}
