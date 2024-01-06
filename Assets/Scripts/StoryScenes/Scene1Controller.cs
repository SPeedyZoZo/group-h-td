using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Controller : MonoBehaviour
{
    public int timeBetweenSceenes;
    void Start()
    {
        Invoke("LoadScene2", timeBetweenSceenes);
    }

    void LoadScene2()
    {
        SceneManager.LoadScene("Scenes/Story/Story2");
    }
}
