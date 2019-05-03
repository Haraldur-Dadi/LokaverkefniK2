using UnityEngine;

public class Resource : MonoBehaviour {

    public Island islandSpawnedOn;

    private bool harvested = false;
    public int maxHp;
    public int hp;

    public GameObject hitFx;
    public GameObject destroyedFx;

    void Start() {
        hp = maxHp;
    }

    public void TakeDamage(int damage) {
        if(hp > 0) {
            hp -= damage;
            if(hitFx != null) {
                Instantiate(hitFx, transform.position, Quaternion.identity);
            }
        } else {
            harvested = true;
            RsMined();
        }
    }

    private void RsMined() {
        if(destroyedFx != null) {
            Instantiate(destroyedFx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
