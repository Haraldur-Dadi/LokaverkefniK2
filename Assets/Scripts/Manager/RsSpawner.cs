using UnityEngine;
using System.Collections;

public class RsSpawner : MonoBehaviour {

    public GameObject[] rs;
    public int randRs;
    public int startWait;

    private Tile[] spawnTiles;

    public Vector3 hitDir = new Vector3(0, 0, 1);
    public LayerMask ground;
    public LayerMask obstacle;
    public bool lookingForTile = true;
    public bool validPosition = true;

    private void Start() {
        spawnTiles = Island.Instance.tiles;
        StartCoroutine(StartRsSpawner());
    }

    public void Update() {
        if (Island.Instance.spawnWait <= 0) {
            StartCoroutine(LookForTileToSpawn());
        }
    }

    IEnumerator StartRsSpawner() {

        int rsCount = 0;

        foreach (Tile tile in spawnTiles) {
            if (rsCount == 10) {
                break;
            } else {
                float spawnChance = Random.Range(0, 25);

                if (tile.rs == null && tile.spawnChance >= spawnChance) {
                    randRs = Random.Range(0, rs.Length);
                    Vector3 spawnPos = tile.transform.position + rs[randRs].transform.position;
                    Instantiate(rs[randRs], spawnPos, Quaternion.identity, tile.transform);
                    tile.rs = rs[randRs];
                    tile.Tier();
                    rsCount += 1;
                }
            }
        }
        yield return null;
    }

    IEnumerator LookForTileToSpawn() {

        lookingForTile = true;
        while (lookingForTile) {

            int tileToSpawn = Random.Range(0, spawnTiles.Length);
            float spawnChance = Random.Range(0, 25);
            Tile tile = spawnTiles[tileToSpawn];

            if (!tile.rs && tile.spawnChance >= spawnChance) {
                randRs = Random.Range(0, rs.Length);
                Vector3 spawnPos = tile.transform.position + rs[randRs].transform.position;
                Instantiate(rs[randRs], spawnPos, Quaternion.identity, tile.transform);
                tile.rs = rs[randRs];
                tile.Tier();
                lookingForTile = false;
            }
        }

        Island.Instance.TimeBtwSpawn();
        yield return null;
    }
}
