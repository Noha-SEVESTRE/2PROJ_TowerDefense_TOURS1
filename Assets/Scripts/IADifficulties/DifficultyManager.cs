using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public GameObject IAEasyManagement;
    public GameObject IANormalManagement;
    public GameObject IAHardManagement;
    public GameObject IAHellManagement;

    private GameObject activeAI;

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("GameDifficulty");

        switch (difficulty)
        {
            case "Easy":
                IAEasyManagement.SetActive(true);
                activeAI = IAEasyManagement;
                break;
            case "Normal":
                IANormalManagement.SetActive(true);
                activeAI = IANormalManagement;
                break;
            case "Hard":
                IAHardManagement.SetActive(true);
                activeAI = IAHardManagement;
                break;
            case "Hell":
                IAHellManagement.SetActive(true);
                activeAI = IAHellManagement;
                break;
            default:
                Debug.LogError("Difficulté inconnue: " + difficulty);
                break;
        }
    }

    public void DeactivateActiveAI()
    {
        if (activeAI != null)
        {
            activeAI.SetActive(false);
        }
    }
}
