using UnityEngine;

public class Tile : MonoBehaviour {

    [Header("Spawn config")]
    public int tier;
    public Biomes biome;
    public GameObject rs;

    [Header("Spawn times")]
    public float spawnWait;
    public float spawnMaxWait;
    public float spawnMinWait;

    private void Start() {
        tier = 1;
        spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
    }

    public void TimeBtwSpawn() {
        if (spawnWait <= 0) {
            spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
        } else {
            spawnWait -= Time.deltaTime;
        }
    }
}
public enum Biomes { Grass, Desert, Winter, Graveyard, Fire };