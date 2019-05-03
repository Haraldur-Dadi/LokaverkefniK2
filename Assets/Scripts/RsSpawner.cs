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
        foreach (Tile tile in Island.Instance.tiles) {
            if (tile.spawnWait == 0 && tile.rs == null) {
                randRs = Random.Range(0, rs.Length);
                Vector3 spawnPos = tile.transform.position + rs[randRs].transform.position;
                Instantiate(rs[randRs], spawnPos, Quaternion.identity, tile.transform);
                tile.rs = rs[randRs];
            }
        }
        yield return null;
    }

    IEnumerator LookForIslandToSpawn() {

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
