using UnityEngine;
using UnityEngine.UI;

public class TurretMenu : MonoBehaviour
{
    public GameObject panel;
    private TurretBuildManager turretBuildManager;

    void Start()
    {
        turretBuildManager = TurretBuildManager.instance;
    }
    public void OpenPanel()
    {
        if(panel != null)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("The panel to open has not been defined !");
        }
    }

    public void ClosePanel()
    {
        if(panel != null)
        {
            panel.SetActive(false);
            turretBuildManager.SetTurretToBuild(null);
        }
        else
        {
            Debug.LogWarning("The panel to close has not been defined !");
        }
    }
}
