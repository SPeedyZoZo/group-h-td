using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;

    private bool inittedMusicVolume = false;

    public float musicVolume
    {
        get
        {
            if (!inittedMusicVolume)
                music.volume = PlayerPrefs.GetFloat("musicVolume", 0.2f);
            return music.volume;
        }
        set
        {
            music.volume = value;
            PlayerPrefs.SetFloat("musicVolume", music.volume);
            PlayerPrefs.Save();
        }
    }

    static private bool inittedEffectVolume = false;
    static private float cachedEffectVolume;
    static public float effectVolume
    {
        get
        {
            if (!inittedEffectVolume)
                cachedEffectVolume = PlayerPrefs.GetFloat("effectVolume", 1f);
            return cachedEffectVolume;
        }
        set
        {
            cachedEffectVolume = value;
            PlayerPrefs.SetFloat("effectVolume", cachedEffectVolume);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        music.volume = musicVolume;
    }

    public static void PlayEffect(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position, cachedEffectVolume);
    }
}
