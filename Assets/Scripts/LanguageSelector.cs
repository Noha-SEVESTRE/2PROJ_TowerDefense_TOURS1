using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    public GameObject languagesPanel; // Référence vers le panneau "LanguagesPanel"

    private bool active = false;

    void Start()
    {
        // Assurez-vous que la langue par défaut est l'anglais (ou une autre langue de votre choix)
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
    }

    public void ChangeLocale(int localeID)
    {
        if(active == true)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {   
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale=LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;

        // Fermer le panneau des langues après avoir sélectionné une langue
        if (languagesPanel != null)
        {
            languagesPanel.SetActive(false);
        }
    }

    // Fonction pour ouvrir le panneau des langues
    public void OpenLanguagesPanel()
    {
        if (languagesPanel != null)
        {
            languagesPanel.SetActive(true);
        }
    }
}
