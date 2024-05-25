using UnityEngine;

public class TurretBuildManager : MonoBehaviour
{
    public static TurretBuildManager instance;
    public GameObject smallTurretPrefab;
    private GameObject turretToBuild;

    private void Start()
    {
        turretToBuild = smallTurretPrefab;
    }

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
}
