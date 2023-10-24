using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public int goalWave;
    public TMPro.TextMeshProUGUI winText;

    public float timeBetweenWaves;
    public float timeBetweenSpawns;
    public float countDown;
    public TMPro.TextMeshProUGUI waveCountdownText;

    private int waveIndex = 0;

    void Update() 
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Floor(countDown).ToString();
    }

    IEnumerator SpawnWave () 
    {
        waveIndex++;

        if (waveIndex >= goalWave)
        {
            Time.timeScale = 0;
            Debug.Log("Win");
            winText.enabled = true;
        }

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns); 
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
