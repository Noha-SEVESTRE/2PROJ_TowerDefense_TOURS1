using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;

    void Update()
    {
        moneyText.text = "Gold: " + PlayerStats.money.ToString();
    }
}
