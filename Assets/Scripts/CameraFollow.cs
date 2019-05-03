using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;

    public Vector3 offset;

    private void Start() {
        offset = new Vector3(0, 0, -10);
        transform.position = player.position + offset;
    }
}
