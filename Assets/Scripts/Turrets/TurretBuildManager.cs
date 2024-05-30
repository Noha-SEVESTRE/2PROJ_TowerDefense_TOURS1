using UnityEngine;

public class TurretBuildManager : MonoBehaviour
{
    public static TurretBuildManager instance;
    public GameObject smallTurretPrefab;
    public GameObject MediumTurretPrefab;
    public GameObject BigTurretPrefab;
    public GameObject GigaTurretPrefab;
    private GameObject turretToBuild;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("There is already a BuildManager in the scene !");
            return;
        }
        instance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
