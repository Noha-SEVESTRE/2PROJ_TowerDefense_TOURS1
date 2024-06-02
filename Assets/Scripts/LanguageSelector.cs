using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    public GameObject languagesPanel;

    private bool active = false;

    void Start()
    {
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

        if (languagesPanel != null)
        {
            languagesPanel.SetActive(false);
        }
    }

    public void OpenLanguagesPanel()
    {
        if (languagesPanel != null)
        {
            languagesPanel.SetActive(true);
        }
    }
}
