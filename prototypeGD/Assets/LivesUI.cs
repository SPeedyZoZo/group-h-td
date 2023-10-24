using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesUI : MonoBehaviour
{

    public TMPro.TextMeshProUGUI livesText;

    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + " Lives";
    }
}
