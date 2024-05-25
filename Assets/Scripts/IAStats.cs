using UnityEngine;

public class IAStats : MonoBehaviour
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

    public static bool CanAfford(int unitCost)
    {
        return money >= unitCost;
    }

    public static void SpendMoney(int amount)
    {
        money -= amount;
    }

    public static void AddMoney(int amount)
    {
        money += amount;
    }

    public static void AddExp(int amount)
    {
        exp += amount;
    }
}
