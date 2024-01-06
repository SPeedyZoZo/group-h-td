using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public string nextScene;
    public float delay;

    private void Start()
    {
        Invoke("LoadNextScene", delay);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
