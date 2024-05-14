using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IDamageable
{
    public int maxHealth = 500;
    public int MaxHealth => maxHealth;
}
