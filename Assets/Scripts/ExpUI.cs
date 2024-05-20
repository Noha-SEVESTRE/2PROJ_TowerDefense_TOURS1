using UnityEngine;
using TMPro;

public class ExpUI : MonoBehaviour
{
    public TMP_Text expText;

    // Update is called once per frame
    void Update()
    {
        expText.text = "EXP: " + PlayerStats.exp.ToString();
    }
}
