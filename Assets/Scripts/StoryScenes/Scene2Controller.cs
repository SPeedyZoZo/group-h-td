using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Controller : MonoBehaviour
{
    public int timeBetweenScenes;
    void Start()
    {
        Invoke("LoadMainMenu", timeBetweenScenes);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/Menu/MainMenu");
    }
}
