using UnityEngine;

public class Island : MonoBehaviour {

    public static Island Instance;
    public GameObject island;
    public Tile[] tiles;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        tiles = GetComponentsInChildren<Tile>();
    }

    private void Update() {
        tiles = GetComponentsInChildren<Tile>();
    }
}