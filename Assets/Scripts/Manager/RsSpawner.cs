using UnityEngine;
using System.Collections;

public class RsSpawner : MonoBehaviour {

    public GameObject[] rs;
    public int randRs;
    public int startWait;

    public Vector3 hitDir = new Vector3(0, 0, 1);
    public LayerMask ground;
    public LayerMask obstacle;
    public bool validPosition = true;

    private void Start() {
        StartCoroutine(StartRsSpawner());
    }

    public void Update() {
        StartCoroutine(LookForIslandToSpawn());
    }

    IEnumerator StartRsSpawner() {

        int rsCount = 0;

        foreach (Tile tile in Island.Instance.tiles) {
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

    IEnumerator LookForIslandToSpawn() {
        Island.Instance.TimeBtwSpawn();
        yield return null;
    }

    void CheckIfIslandHit(Ray dir) {
        RaycastHit2D collider = Physics2D.GetRayIntersection(dir, Mathf.Infinity, obstacle);

        if (collider.collider != null && collider.collider.transform.CompareTag("Rs")) {
            Debug.Log("Hit resource... not placing");
            validPosition = false;
        } else {
            validPosition = true;
        }
    }
}
