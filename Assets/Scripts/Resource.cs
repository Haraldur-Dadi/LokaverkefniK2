using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour {

    public Island islandSpawnedOn;

    private bool harvested = false;
    public float maxHp;
    public float hp;
    
    public GameObject hpBarGO;
    public Slider hpBarSlider;

    public float timeSinceLastHit;

    public GameObject hitFx;
    public GameObject destroyedFx;
    public Vector3 fxOffset;

    [Header("Drop")]
    public GameObject materialToDrop;
    public int nrMaterialsToDrop;
    public int maxDrop;
    public int minDrop;

    void Start() {
        hp = maxHp;
        hpBarGO.SetActive(false);
        nrMaterialsToDrop = Random.Range(minDrop, maxDrop);
    }

    private void Update() {
        if(timeSinceLastHit > 0) {
            timeSinceLastHit -= Time.deltaTime;
            ShowHpBar();
        }
    }

    public void TakeDamage(int damage) {
        if(hp > 1) {
            timeSinceLastHit = 3;
            hp -= damage;
            if(hitFx != null) {
                Instantiate(hitFx, transform.position - fxOffset, Quaternion.identity, transform);
            }
            MainCamera.Instance.CameraShake();
        } else {
            harvested = true;
            RsMined();
        }
    }

    public void ShowHpBar() {
        hpBarGO.SetActive(true);
        if(timeSinceLastHit > 0) {
            hpBarSlider.value = hp / maxHp;
        } else {
            hpBarGO.SetActive(false);
        }
    }

    private void RsMined() {
        float destroyTime = 3;
        for (int i = 0; i < nrMaterialsToDrop; i++) {
            if(nrMaterialsToDrop < 5) {
                Instantiate(materialToDrop, transform.position, Quaternion.identity);
            }
        }
        if (destroyedFx != null) {
            gameObject.SetActive(false);
            Instantiate(destroyedFx, transform.position - fxOffset, Quaternion.identity);
        }
        Destroy(gameObject, destroyTime);
    }
}
