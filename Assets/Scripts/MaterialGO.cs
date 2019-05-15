using UnityEngine;

public class MaterialGO : MonoBehaviour {

    public Item material;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Inventory.Instance.AddItem(material);
            Destroy(gameObject);
        }
    }
    private void OnMouseEnter() {
        Inventory.Instance.AddItem(material);
        Destroy(gameObject);
    }
}
