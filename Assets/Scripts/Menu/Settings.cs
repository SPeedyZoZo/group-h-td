using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;

    public AudioClip effectClip;
    private bool shouldPlayEffect;

    private void Start()
    {
        musicVolumeSlider.value = audioManager.musicVolume;
        effectVolumeSlider.value = AudioManager.effectVolume;
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
