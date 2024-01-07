using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public float delay;

    private void Start()
    {
        Invoke("LoadNextScene", delay);
    }

    public void LoadNextScene()
    {
        GameState.level++;
        SceneManager.LoadScene("Level" + GameState.level);
    }
}
