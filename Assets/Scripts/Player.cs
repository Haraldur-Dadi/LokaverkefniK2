using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    public GameObject[] equipable;
    public GameObject equipedItem;

    public LayerMask layerMask;
    public float attack = 1;
    public float attackRange = 1f;
    public float attackSpeed;
    public float timeBtwMiningHit = 0;
    public float staminaUsed;

    public Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        for (int i = 0; i < equipable.Length; i++) {
            if(i == 0) {
                equipable[i].SetActive(true);
                equipable[i].GetComponent<SpriteRenderOrderSystem>().updateSorting = true;
            } else {
                equipable[i].SetActive(false);
                equipable[i].GetComponent<SpriteRenderOrderSystem>().updateSorting = false;
            }
        }
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            for (int i = 0; i < equipable.Length; i++) {
                if (i == 0) {
                    equipable[i].SetActive(true);
                    equipable[i].GetComponent<SpriteRenderOrderSystem>().updateSorting = true;
                } else {
                    equipable[i].SetActive(false);
                    equipable[i].GetComponent<SpriteRenderOrderSystem>().updateSorting = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            for (int i = 0; i < equipable.Length; i++) {
                if (i == 1) {
                    equipable[i].SetActive(true);
                    equipable[i].GetComponent<SpriteRenderOrderSystem>().updateSorting = true;
                } else {
                    equipable[i].SetActive(false);
                    equipable[i].GetComponent<SpriteRenderOrderSystem>().updateSorting = false;
                }
            }
        }

        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (Input.GetMouseButtonDown(0) && timeBtwMiningHit <= 0) {
                RaycastHit2D hit;
                anim.SetBool("Attack", false);
                anim.SetBool("Attack", true);
                timeBtwMiningHit = attackSpeed;

                if (Movement.Instance.isFacingLeft) {
                    hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange, layerMask);
                } else {
                    hit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, layerMask);
                }
                if (hit.collider != null) {
                    if (hit.collider.CompareTag("Rs")) {
                        hit.collider.GetComponent<Resource>().TakeDamage(attack);
                    }
                    if (hit.collider.CompareTag("Enemy")) {
                        hit.collider.GetComponent<Enemy>().TakeDamage(attack);
                    }
                }

                Stamina.Instance.ReduceStamina(staminaUsed);
            }
        }

        timeBtwMiningHit -= Time.deltaTime;
    }
}
