using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public float delay;
        public float interval;
        public Enemy[] enemies;
    }

    public int initialMoney = 400;
    public static int money;

    public float nextLevelDelay = 3f;
    public Transform spawn;
    public Wave[] waves;

    private static float nextWaveTime;
    private int activeEnemies = 0;

    private void Start()
    {
        money = initialMoney;

        Enemy.onDeath += OnEnemyDeath;
        Enemy.onDealDamage += OnEnemyDealDamage;

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in waves)
        {
            activeEnemies = wave.enemies.Length;

            nextWaveTime = Time.time + wave.delay;
            yield return new WaitForSeconds(nextWaveTime - Time.time);

            foreach (Enemy enemy in wave.enemies)
            {
                Instantiate(enemy, spawn.position, spawn.rotation);
                yield return new WaitForSeconds(wave.interval);
            }

            yield return new WaitUntil(() => activeEnemies == 0);
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
        activeEnemies--;
    }

    private void OnEnemyDealDamage(float damage)
    {
        activeEnemies--;
        GameState.lives--;

        if (GameState.lives == 0)
            SceneManager.LoadScene("GameOver");
    }

    public static float GetCountdown()
    {
        return Time.time > nextWaveTime ? 0f : nextWaveTime - Time.time;
    }
}
