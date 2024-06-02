using UnityEngine;
using UnityEngine.UI;

public class LanguagePanel : MonoBehaviour
{
    public GameObject panel; // Référence vers le panel que vous souhaitez activer/désactiver
    private bool panelActive = false; // État actuel du panel

    void Start()
    {
        // Assurez-vous que le panel est désactivé au démarrage du jeu
        panel.SetActive(false);
    }

    public void TogglePanel()
    {
        // Inverse l'état du panel à chaque clic sur le bouton
        panelActive = !panelActive;

        // Active ou désactive le panel en fonction de son état
        panel.SetActive(panelActive);
    }
}
