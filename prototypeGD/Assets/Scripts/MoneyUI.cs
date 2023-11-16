using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI MoneyText;

    void Update()
    {
        MoneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
