using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
    public class Wave {
        public GameObject[] enemy;
        public int count;
        public float rate;
    }
    public static int EnemiesAlive;
    public Wave[] waves;
    public static Transform spawnPoint;
    public float timeBetweenWaves = 90f;
    private float countdown;
    public TextMeshProUGUI waveCountdownText;
    public GameObject countdownTxt;
    private int waveIndex = 0;

    private void Start() {
        EnemiesAlive = 0;
        countdown = 1f;
    }

    private void Update() {
        if(GameManager.isPaused == true) {
            return;
        }
        if (EnemiesAlive > 0) {
            return;
        }
        if (waveIndex == waves.Length) {
            enabled = false;
            Debug.Log("You have won!");
        }
        if (countdown <= 0f) {
            countdownTxt.SetActive(false);
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdownTxt.SetActive(true);
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    IEnumerator SpawnWave() {

        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++) {
            int randEnemySpawn = Random.Range(0, wave.enemy.Length);
            SpawnEnemy(wave.enemy[randEnemySpawn]);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }
    void SpawnEnemy(GameObject enemy) {
        Wave wave = waves[waveIndex];
        int randSpawnPos = Random.Range(0, Island.Instance.tiles.Length);
        spawnPoint = Island.Instance.tiles[randSpawnPos].transform;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
