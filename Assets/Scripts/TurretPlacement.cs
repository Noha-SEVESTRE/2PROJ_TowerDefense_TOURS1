using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    private SpriteRenderer rend;
    private GameObject turret;
    
    private TurretBuildManager turretBuildManager;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;

        turretBuildManager = TurretBuildManager.instance;
    }
    private void OnMouseEnter()
    {
        if(turretBuildManager.GetTurretToBuild() == null)
        {
            return;
        }

        rend.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.color = startColor;
    }

    private void OnMouseDown()
    {
        if(turretBuildManager.GetTurretToBuild() == null)
        {
            return;
        }

        if(turret != null)
        {
            Debug.Log("Can't place, a turret is already here.");
            return;
        }

        GameObject turretToBuild = turretBuildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);

        turretBuildManager.SetTurretToBuild(null);
    }
}
