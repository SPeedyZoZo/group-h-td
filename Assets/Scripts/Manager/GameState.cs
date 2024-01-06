using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public int startingLives;
    private static int staticStartingLives;
    private static int cachedLives;
    private static int cachedLevel;
    private static Difficulty cachedDifficulty;

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

    static private bool inittedDifficulty = false;
    static public Difficulty difficulty
    {
        get
        {
            if (!inittedDifficulty)
                cachedDifficulty = (Difficulty)PlayerPrefs.GetInt("difficulty", (int)Difficulty.Medium);
            return cachedDifficulty;
        }
        set
        {
            cachedDifficulty = value;
            PlayerPrefs.SetInt("difficulty", (int)cachedDifficulty);
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