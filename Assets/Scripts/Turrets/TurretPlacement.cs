using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    private SpriteRenderer rend;
    private GameObject turret;
    
    private TurretBuildManager turretBuildManager;
    private Evolution evolutionScript;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;

        turretBuildManager = TurretBuildManager.instance;
        evolutionScript = FindObjectOfType<Evolution>();
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
            turretBuildManager.SetTurretToBuild(null);
            return;
        }

        GameObject turretToBuild = turretBuildManager.GetTurretToBuild();
        Turret turretComponent = turretToBuild.GetComponent<Turret>();
        int turretPrice = turretComponent.price;

        if(turretPrice > PlayerStats.money)
        {
            Debug.Log("Not enough money to place the turret.");
            turretBuildManager.SetTurretToBuild(null);
            return;
        }

        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        turret.tag = "TurretPlayer1";

        // Déterminer la couleur en fonction du niveau du joueur 1
        if (evolutionScript != null)
        {
            SpriteRenderer turretSpriteRenderer = turret.GetComponent<SpriteRenderer>();
            if (turretSpriteRenderer != null)
            {
                turretSpriteRenderer.color = evolutionScript.DeterminePlayerColor(evolutionScript.Player1Level, evolutionScript.Player1Level);
            }
            else
            {
                Debug.LogWarning("Le SpriteRenderer n'a pas été trouvé sur la tourelle instanciée.");
            }
        }
        else
        {
            Debug.LogWarning("Le script Evolution n'a pas été trouvé.");
        }

        PlayerStats.money -= turretPrice;

        turretBuildManager.SetTurretToBuild(null);
    }
}