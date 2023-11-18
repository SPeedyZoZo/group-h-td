using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public GameManager gameManager; // Reference to the GameManager

    public int goalWave;

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

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave () 
    {
        waveIndex++;

        if (waveIndex >= goalWave)
        {
            gameManager.gameWon();
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
