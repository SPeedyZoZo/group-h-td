using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;
    public TMP_Dropdown difficultyDropdown;

    public AudioClip effectClip;
    private bool shouldPlayEffect;

    private void Start()
    {
        musicVolumeSlider.value = audioManager.musicVolume;
        effectVolumeSlider.value = AudioManager.effectVolume;
        difficultyDropdown.value = (int)GameState.difficulty;
    }

    public void OnMusicVolumeChange(float value)
    {
        audioManager.musicVolume = value;
    }

    public void OnEffectVolumeChange(float value)
    {
        AudioManager.effectVolume = value;
    }

    public void OnEffectVolumePointerDown()
    {
        StartCoroutine(PlayEffect());
    }

    public void OnEffectVolumePointerUp()
    {
        shouldPlayEffect = false;
    }

    public void OnDifficultyChange(int value)
    {
        GameState.difficulty = (GameState.Difficulty)value;
    }

    private IEnumerator PlayEffect()
    {
        shouldPlayEffect = true;
        while (shouldPlayEffect)
        {
            AudioManager.PlayEffect(effectClip, Vector3.zero);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
