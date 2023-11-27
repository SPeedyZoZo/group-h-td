using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Button saveButton;
    public Button backButton;
    public AudioSource audioSource;
    public AudioClip volumeChangeClip;

    private Coroutine playSoundCoroutine;

    void Start()
    {
        // Load previously saved volume
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f); // Default to 100% volume if not set

        // Attach a listener to the volume slider to handle the volume change
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        // Initialize the EventTrigger and add PointerDown and PointerUp event listeners
        EventTrigger trigger = volumeSlider.gameObject.AddComponent<EventTrigger>();

        var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown(); });
        trigger.triggers.Add(pointerDownEntry);

        var pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp(); });
        trigger.triggers.Add(pointerUpEntry);

        saveButton.onClick.AddListener(SaveSettings);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    // This method will be called whenever the volume slider's value changes
    public void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }

    // Called when the user starts dragging the slider or clicks on it
    private void OnPointerDown()
    {
        if (playSoundCoroutine != null)
        {
            StopCoroutine(playSoundCoroutine);
        }
        playSoundCoroutine = StartCoroutine(PlaySoundWhileInteracting());
    }

    // Called when the user stops dragging the slider or releases the click
    private void OnPointerUp()
    {
        if (playSoundCoroutine != null)
        {
            StopCoroutine(playSoundCoroutine);
        }
    }

    private IEnumerator PlaySoundWhileInteracting()
    {
        // Play the initial sound immediately upon interaction
        if (audioSource != null && volumeChangeClip != null)
        {
            audioSource.PlayOneShot(volumeChangeClip);
        }
        // Continue playing the sound every 0.5 seconds while interacting
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (audioSource != null && volumeChangeClip != null)
            {
                audioSource.PlayOneShot(volumeChangeClip);
            }
        }
    }

    // This method saves the volume setting
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    // Call this method to go back to the Main Menu
    private void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
