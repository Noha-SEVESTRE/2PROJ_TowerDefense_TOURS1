using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlotBuying : MonoBehaviour
{
    public GameObject[] turretPositions;
    public int[] turretPrices;
    private int currentIndex = 0;

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
}
