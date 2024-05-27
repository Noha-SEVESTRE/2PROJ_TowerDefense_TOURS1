using UnityEngine;
using UnityEngine.UI;

public class TurretMenu : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject turretPositions;
    private TurretBuildManager turretBuildManager;

    void Start()
    {
        turretBuildManager = TurretBuildManager.instance;
    }
    public void OpenPanels()
    {
        if(shopPanel != null && turretPositions != null)
        {
            shopPanel.SetActive(true);
            turretPositions.SetActive(true);
        }
        else
        {
            Debug.LogWarning("The panels to open have not been defined !");
        }
    }

    public void ClosePanels()
    {
        if(shopPanel != null && turretPositions != null)
        {
            shopPanel.SetActive(false);
            turretPositions.SetActive(false);
            turretBuildManager.SetTurretToBuild(null);
        }
        else
        {
            Debug.LogWarning("The panels to close have not been defined !");
        }
    }
}
