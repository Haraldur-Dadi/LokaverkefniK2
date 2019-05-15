using UnityEngine;

public class Island : MonoBehaviour {

    public static Island Instance;
    public GameObject island;
    public Tile[] tiles;

    [Header("Spawn times")]
    public float spawnWait;
    public float spawnMaxWait;
    public float spawnMinWait;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        tiles = GetComponentsInChildren<Tile>();
        spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
    }

    private void Update() {
        tiles = GetComponentsInChildren<Tile>();
        TimeBtwSpawn();
    }

    public void TimeBtwSpawn() {
        if (spawnWait <= 0) {
            spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
        } else {
            spawnWait -= Time.deltaTime;
        }
    }
}