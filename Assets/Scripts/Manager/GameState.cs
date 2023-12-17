using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int startingLives;
    private static int staticStartingLives;
    private static int cachedLives;
    private static int cachedLevel;

    private void Start()
    {
        staticStartingLives = startingLives;
        cachedLives = PlayerPrefs.GetInt("lives", startingLives);
        cachedLevel = PlayerPrefs.GetInt("level", 0);
    }

    static public int lives
    {
        get { return cachedLives; }
        set
        {
            cachedLives = value;
            PlayerPrefs.SetInt("lives", cachedLives);
            PlayerPrefs.Save();
        }
    }

    static public int level
    {
        get { return cachedLevel; }
        set
        {
            cachedLevel = value;
            PlayerPrefs.SetInt("level", cachedLevel);
            PlayerPrefs.Save();
        }
    }

    static public void Reset()
    {
        cachedLives = staticStartingLives;
        cachedLevel = 0;

        PlayerPrefs.SetInt("lives", cachedLives);
        PlayerPrefs.SetInt("level", cachedLevel);
        PlayerPrefs.Save();
    }
}