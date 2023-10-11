using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text WaveCountdownText;
    public Text WaveCountText;

    public GameManager gameManager;

    private int waveNumber = 0;

    System.Random ranNumGenerator = new System.Random();

    private void Start()
    {
        countdown = 5f;
        EnemiesAlive = 1;
    }

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveNumber == waves.Length && PlayerStats.Lives > 0)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        WaveCountdownText.text = string.Format("{0:00.00}", countdown);
        WaveCountText.text = "Wave: " + (waveNumber + 1) + "/" + waves.Length;
        if (GameManager.GameIsOver)
        {
            WaveCountText.text = "Complete!";
            this.enabled = false;
        }
    }

    IEnumerator SpawnWave ()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];

        //EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            FindObjectOfType<AudioManager>().Play("EnemySpawn");
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
    }

    void SpawnEnemy (GameObject enemy)
    {
        Vector3 offSet = new Vector3(ranNumGenerator.Next(-2, 3), 0, ranNumGenerator.Next(-2, 3));
        Instantiate(enemy, spawnPoint.position + offSet, spawnPoint.rotation); // -2 and 3 because the top number is non inclusive. So it's -2 and 2.
        enemy.GetComponent<EnemyMovement>().offSet = offSet;
        EnemiesAlive++;
    }
}
