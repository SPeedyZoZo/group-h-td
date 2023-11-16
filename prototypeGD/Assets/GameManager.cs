using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI loseText;
    public TMPro.TextMeshProUGUI winText;
    private bool gameEnded = false;

    void Start()
    {
        loseText.enabled = false;
    }
    void Update()
    {
        if (gameEnded) return;
        if (PlayerStats.Lives <= 0) {
            EndGame();
        }
    }

    void EndGame() {
        gameEnded = true;
        Debug.Log("game over, display lose message");
        loseText.enabled = true;
        Time.timeScale = 0;
    }

    public void gameWon()
    {
        Debug.Log("GameWon");
        Time.timeScale = 0;
        Debug.Log("Win");
        winText.enabled = true;
    }
}
