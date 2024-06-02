using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurretSlotBuying : MonoBehaviour
{
    public GameObject[] turretPositions;
    public int[] turretPrices;
    private int currentIndex = 0;
    public TMP_Text priceText;

    public void ActivateNextTurretPosition()
    {
        if (currentIndex >= 0 && currentIndex < turretPositions.Length)
        {
            if (PlayerStats.money >= turretPrices[currentIndex])
            {
                turretPositions[currentIndex].SetActive(true);

                PlayerStats.money -= turretPrices[currentIndex];

                currentIndex++;
            }
            else
            {
                Debug.LogWarning("Pas assez d'argent pour acheter cet emplacement de tourelle !");
            }
        }
        else
        {
            Debug.LogWarning("Indice d'emplacement de tourelle non valide !");
        }
    }

    private void Update()
    {
        if (currentIndex < turretPrices.Length)
        {
            priceText.text = "Buy Slot " + turretPrices[currentIndex].ToString(); // Met à jour le texte avec le prix actuel
        }
        else
        {
            priceText.text = "-";
        }
    }
}
