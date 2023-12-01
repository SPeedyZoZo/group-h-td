using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Button saveButton;
    public Button backButton;
    public Toggle fullscreenToggle; 
    public AudioSource audioSource;
    public AudioClip volumeChangeClip;
    public TMP_Dropdown graphicsQualityDropdown;

    private Coroutine playSoundCoroutine;

    void Start()
    {
        // Set up graphics quality dropdown
        graphicsQualityDropdown.AddOptions(QualitySettings.names.ToList());
        int savedQualityIndex = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        graphicsQualityDropdown.value = savedQualityIndex;
        QualitySettings.SetQualityLevel(savedQualityIndex);
        graphicsQualityDropdown.onValueChanged.AddListener(SetGraphicsQuality);

        // Load previously saved volume
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f); // Default to 100% volume if not set
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        // Load the state of the fullscreen toggle
        bool isFullscreen = PlayerPrefs.GetInt("FullscreenToggle", 0) == 1;
        Screen.fullScreen = isFullscreen; // Apply the saved fullscreen state
        fullscreenToggle.isOn = isFullscreen; // Update the toggle UI

        // Initialize EventTrigger for volume slider
        EventTrigger trigger = volumeSlider.gameObject.AddComponent<EventTrigger>();
        var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown(); });
        trigger.triggers.Add(pointerDownEntry);
        var pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp(); });
        trigger.triggers.Add(pointerUpEntry);

        // Set up fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullScreen);

        // Setup button listeners
        saveButton.onClick.AddListener(SaveSettings);
        backButton.onClick.AddListener(BackToMainMenu);


    }

    public void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        // Optionally save the fullscreen state immediately
        PlayerPrefs.SetInt("FullscreenToggle", isFullScreen ? 1 : 0);
    }

    private void OnPointerDown()
    {
        if (playSoundCoroutine != null)
        {
            StopCoroutine(playSoundCoroutine);
        }
        playSoundCoroutine = StartCoroutine(PlaySoundWhileInteracting());
    }

    private void OnPointerUp()
    {
        if (playSoundCoroutine != null)
        {
            StopCoroutine(playSoundCoroutine);
        }
    }

    private IEnumerator PlaySoundWhileInteracting()
    {
        if (audioSource != null && volumeChangeClip != null)
        {
            audioSource.PlayOneShot(volumeChangeClip);
        }
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (audioSource != null && volumeChangeClip != null)
            {
                audioSource.PlayOneShot(volumeChangeClip);
            }
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("GraphicsQuality", graphicsQualityDropdown.value);
        PlayerPrefs.SetInt("FullscreenToggle", fullscreenToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();
    }


    private void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
