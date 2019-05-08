using UnityEngine;

public class Cleaner : MonoBehaviour {

    public float destroyTime;

    public void Start() {
        Destroy(gameObject, destroyTime);
    }
}
