using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public static int exp;
    public int startMoney = 2500;
    public int startExp = 1000;

    public void Start()
    {
        money = startMoney;
        exp = startExp;
    }
}
