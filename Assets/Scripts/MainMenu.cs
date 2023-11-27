using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the correct scene (will be assigned to buttons)
    public void Play()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Level1"); 
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
