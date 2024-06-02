using UnityEngine;
using TMPro;

public class GameSpeed : MonoBehaviour
{
    private bool isSpeedIncreased = false;
    private float normalSpeed = 1.0f;
    private float increasedSpeed = 2.0f;
    public TextMeshProUGUI speedText;

    void Start()
    {
        UpdateSpeedText();
    }

    public void ToggleGameSpeed()
    {
        if (isSpeedIncreased)
        {
            Time.timeScale = normalSpeed;
            isSpeedIncreased = false;
        }
        else
        {
            Time.timeScale = increasedSpeed;
            isSpeedIncreased = true;
        }

        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        if (isSpeedIncreased)
        {
            speedText.text = "X2";
        }
        else
        {
            speedText.text = "X1";
        }
    }
}
