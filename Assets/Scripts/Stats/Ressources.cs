using UnityEngine;
using TMPro;

public class Ressources : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text expText;


    void Update()
    {
        moneyText.text = "Gold: " + PlayerStats.money.ToString();
        expText.text = "EXP: " + PlayerStats.exp.ToString();
    }
}
