using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI header;

    private void Start()
    {
        header.text = $"Level {GameState.level} Complete";
        GameState.level++;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + GameState.level);
    }
}
