using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLevel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI header;

    private void Start()
    {
        header.text = $"Level {GameState.level} Failed";
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene("Level" + GameState.level);
    }
}
