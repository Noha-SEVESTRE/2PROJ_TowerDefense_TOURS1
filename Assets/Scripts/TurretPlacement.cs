using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    private SpriteRenderer rend;
    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }
    private void OnMouseEnter()
    {
        rend.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.color = startColor;
    }

    private void OnMouseDown()
    {
        if(turret != null)
        {
            Debug.Log("Can't place, a turret is already here.");
            return;
        }

        GameObject turretToBuild = TurretBuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
    }
}
