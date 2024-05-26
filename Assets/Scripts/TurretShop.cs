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

    public void PurchaseMediumTurret()
    {
        turretBuildManager.SetTurretToBuild(turretBuildManager.MediumTurretPrefab);
    }

    public void PurchaseBigTurret()
    {
        turretBuildManager.SetTurretToBuild(turretBuildManager.BigTurretPrefab);
    }

    public void PurchaseGigaTurret()
    {
        turretBuildManager.SetTurretToBuild(turretBuildManager.GigaTurretPrefab);
    }
}
