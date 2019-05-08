using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour {

    private Ray ray;
    public LayerMask layerMask;

    public float miningRange;
    public CircleCollider2D miningDetector;

    public int miningHit = 1;
    public float miningSpeed;
    private float timeBtwMiningHit = 0;
    public float staminaUsed;
    public List<GameObject> minableRs = new List<GameObject>();
    public GameObject rsHit;

    public Animator anim;

    private void Start() {
        miningDetector.radius = miningRange;
        anim = GetComponent<Animator>();
    }

    private void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);
        if (hit2D.collider != null && timeBtwMiningHit <= 0) {
            if (Input.GetMouseButtonDown(0)) {
                rsHit = hit2D.transform.gameObject;
                if (minableRs.Contains(rsHit)) {
                    timeBtwMiningHit = miningSpeed;
                    anim.SetBool("Mining", true);
                    rsHit.GetComponent<Resource>().TakeDamage(miningHit);
                    Stamina.Instance.ReduceStamina(staminaUsed);
                }
            }
        }
        timeBtwMiningHit -= Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Rs") && !minableRs.Contains(other.gameObject)) {
            minableRs.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Rs")) {
            minableRs.Remove(other.gameObject);
        }
    }
}
