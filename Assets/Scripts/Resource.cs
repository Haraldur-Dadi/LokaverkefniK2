using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour {

    public Island islandSpawnedOn;

    public float maxHp;
    public float hp;
    
    public Slider hpBarSlider;

    public float timeSinceLastHit;

    public int nrParticles;
    public Sprite[] particles;
    public GameObject hitFx;
    public ParticleSystem subhitFx;
    public GameObject backgroundHitFx;
    public GameObject destroyedFx;
    public Vector3 fxOffset;

    [Header("Drop")]
    public GameObject materialToDrop;
    public int nrMaterialsToDrop;
    public int maxDrop;
    public int minDrop;

    void Start() {
        hp = maxHp;
        hpBarSlider.gameObject.SetActive(false);
        nrMaterialsToDrop = Random.Range(minDrop, maxDrop);
        subhitFx = hitFx.GetComponentInChildren<ParticleSystem>();
    }

    private void Update() {
        if(timeSinceLastHit > 0) {
            timeSinceLastHit -= Time.deltaTime;
            ShowHpBar();
        }
    }

    public void TakeDamage(float damage) {
        if(hp > 1) {
            timeSinceLastHit = 3;
            hp -= damage;
            if (hitFx != null) {
                if(nrParticles == 1) {
                    int particleToUse = Random.Range(0, particles.Length);
                    hitFx.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, particles[particleToUse]);
                } else {
                    int particleToUse = Random.Range(0, 2); //wood
                    int particleToUse2 = Random.Range(2, 4); //leaves
                    subhitFx.textureSheetAnimation.SetSprite(0, particles[particleToUse2]);
                    hitFx.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, particles[particleToUse]);
                }

                Instantiate(hitFx, transform.position + hitFx.transform.position, Quaternion.identity, transform);
            }
            if(backgroundHitFx != null) {
                Instantiate(backgroundHitFx, transform.position - fxOffset, Quaternion.identity, transform);
            }
            MainCamera.Instance.CameraShake();
        } else { 
            RsMined();
        }
    }

    public void ShowHpBar() {
        hpBarSlider.gameObject.SetActive(true);
        if(timeSinceLastHit > 0) {
            hpBarSlider.value = hp / maxHp;
        } else {
            hpBarSlider.gameObject.SetActive(false);
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
