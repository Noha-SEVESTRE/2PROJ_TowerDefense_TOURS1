using UnityEngine;

public class TurretShop : MonoBehaviour
{
    private TurretBuildManager turretBuildManager;

    private void Start()
    {
        turretBuildManager = TurretBuildManager.instance;
    }

    public void PurchaseSmallTurret()
    {
        turretBuildManager.SetTurretToBuild(turretBuildManager.smallTurretPrefab);
    }
}
