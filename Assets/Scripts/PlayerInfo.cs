using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney;
    public static int Lives;
    public int startLives;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}