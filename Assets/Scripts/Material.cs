using UnityEngine;

public class Material : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
    private void OnMouseEnter() {
        Destroy(gameObject);
    }
}
