using UnityEngine;
using TMPro;

public class ExpUI : MonoBehaviour
{
    public TMP_Text expText;

    void Update()
    {
        expText.text = "EXP: " + PlayerStats.exp.ToString();
    }
}
