using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int initialMoney = 400;
    public static int money;
    public float nextWaveDelay = 5f;
    public float nextLevelDelay = 3f;
    public bool skipNextLevelScreen = false;

    private Spawner[] spawners;
    private static float nextWaveTime;
    private int enemies = 0;

    private void Awake()
    {
        spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
    }

    private void Start()
    {
        money = initialMoney;
        Enemy.onDeath += OnEnemyDeath;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            foreach (Spawner spawner in spawners)
                enemies += spawner.GetNextWaveEnemies();

            if (enemies == 0)
                break;

            nextWaveTime = Time.time + nextWaveDelay;
            yield return new WaitForSeconds(nextWaveTime - Time.time);

            foreach (Spawner spawner in spawners)
                StartCoroutine(spawner.SpawnNextWave());

            yield return new WaitUntil(() => enemies <= 0);
            enemies = 0;
        }

        yield return new WaitForSeconds(nextLevelDelay);
        NextLevel();
    }

    private void NextLevel()
    {
        if (skipNextLevelScreen)
        {
            GameState.level++;
            SceneManager.LoadScene("Level" + GameState.level);
        }
        else
        {
            SceneManager.LoadScene("NextLevel");
        }
    }

    private void OnEnemyDeath()
    {
        enemies--;
    }

    public static float GetCountdown()
    {
        return Time.time > nextWaveTime ? 0f : nextWaveTime - Time.time;
    }
}
