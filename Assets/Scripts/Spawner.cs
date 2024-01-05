using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public float interval;
        public Enemy[] enemies;
    }

    public Wave[] waves;
    private int waveIndex;

    private Transform[] path;

    private void Awake()
    {
        path = new Transform[transform.childCount];
        for (int i = 0; i < path.Length; i++)
            path[i] = transform.GetChild(i);
    }

    public int GetNextWaveEnemies()
    {
        return waveIndex >= waves.Length
            ? 0
            : waves[waveIndex].enemies.Length;
    }

    public IEnumerator SpawnNextWave()
    {
        if (waveIndex >= waves.Length)
            yield break;

        Wave wave = waves[waveIndex++];
        foreach (Enemy enemy in wave.enemies)
        {
            Instantiate(enemy, transform.position, transform.rotation).Seek(path);
            yield return new WaitForSeconds(wave.interval);
        }
    }
}
