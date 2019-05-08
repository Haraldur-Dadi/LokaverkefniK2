using UnityEngine;

public class SpriteRenderOrderSystem : MonoBehaviour {

    SpriteRenderer sprite;
    Canvas canvas;
    ParticleSystemRenderer particleSystemRenderer;

    public float offset;

    public int useSprite;
    public bool updateSorting;

    void Start() {
        if (useSprite == 1) {
            sprite = GetComponent<SpriteRenderer>();

            offset = sprite.sortingOrder;

            sprite.sortingOrder = (int)((sprite.transform.position.y * -100) + offset);
        } else if (useSprite == 2) {
            canvas = GetComponent<Canvas>();

            offset = canvas.sortingOrder;

            canvas.sortingOrder = (int)((canvas.transform.position.y * -100) + offset);
        } else if (useSprite == 3) {
            particleSystemRenderer = GetComponent<ParticleSystemRenderer>();

            offset = particleSystemRenderer.sortingOrder;

            particleSystemRenderer.sortingOrder = (int)((particleSystemRenderer.transform.position.y * -120) + offset);
        }
    }

    private void LateUpdate() {
        if (updateSorting) {
            UpdateSorting();
        }
    }

    public void UpdateSorting() {
        if (useSprite == 1) {
            sprite.sortingOrder = (int)((sprite.transform.position.y * -100) + offset - 50);
        } else if (useSprite == 2) {
            canvas.sortingOrder = (int)((canvas.transform.position.y * -100) + offset);
        } else if (useSprite == 3) {
            particleSystemRenderer.sortingOrder = (int)((particleSystemRenderer.transform.position.y * -100) + offset);
        }
    }
}
