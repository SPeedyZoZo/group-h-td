using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public int lives = 5;
    public Button continueGame;

    private void Start()
    {
        continueGame.interactable = GameState.level != 0;
    }

    public void LoadNewGame()
    {
        GameState.lives = lives;
        GameState.level = 1;

        LoadLevel();
    }

    public void LoadContinueGame()
    {
        LoadLevel();
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Level" + GameState.level);
    }
}
