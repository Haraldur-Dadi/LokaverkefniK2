using UnityEngine;

public class Tile : MonoBehaviour {

    [Header("Spawn config")]
    public int tier;
    public float spawnChance;
    public int startChance;
    public Biomes biome;
    public GameObject rs;

    private void Start() {
        SpawnChance();
        tier = 1;
    }
    private void Update() {
        SpawnChance();
    }

    public void Tier() {
    }
    public void SpawnChance() {
    }
}
public enum Biomes { Grass, Desert, Winter, Graveyard, Fire };