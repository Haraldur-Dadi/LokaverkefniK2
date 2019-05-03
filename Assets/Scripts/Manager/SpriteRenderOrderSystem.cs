using UnityEngine;

public class SpriteRenderOrderSystem : MonoBehaviour {

    SpriteRenderer sprite;
    public float offset;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();

        offset = sprite.sortingOrder;

        sprite.sortingOrder = (int)((sprite.transform.position.y * -100) + offset);
    }

    public void UpdateSorting() {
        sprite.sortingOrder = (int)((sprite.transform.position.y * -100) + offset - 50);
    }
}
