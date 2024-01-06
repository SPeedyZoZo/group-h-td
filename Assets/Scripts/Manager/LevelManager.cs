using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int initialMoney = 400;
    public static int money;
    public float nextWaveDelay = 5f;
    public float nextLevelDelay = 3f;

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
            Debug.Log(enemies);

            if (enemies == 0)
                break;

            nextWaveTime = Time.time + nextWaveDelay;
            yield return new WaitForSeconds(nextWaveTime - Time.time);

            foreach (Spawner spawner in spawners)
                StartCoroutine(spawner.SpawnNextWave());

            yield return new WaitUntil(() => enemies == 0);
        }

        yield return new WaitForSeconds(nextLevelDelay);
        NextLevel();
    }

    private void NextLevel()
    {
        int nextLevel = GameState.level + 1;
        int nextSceneIdx = SceneUtility.GetBuildIndexByScenePath("Level" + nextLevel);

        if (nextSceneIdx == -1)
            SceneManager.LoadScene("GameWon");
        else
            SceneManager.LoadScene("NextLevel");
    }

    private void OnEnemyDeath()
    {
        enemies--;
        Debug.Log(enemies);
    }

    public static float GetCountdown()
    {
        return Time.time > nextWaveTime ? 0f : nextWaveTime - Time.time;
    }
}
