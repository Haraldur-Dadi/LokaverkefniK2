using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour {

    private Ray ray;
    public LayerMask layerMask;

    public float miningRange;
    public CircleCollider2D miningDetector;

    public int miningHit = 1;
    public float staminaUsed;
    public List<GameObject> minableRs = new List<GameObject>();
    public GameObject rsHit;

    private void Start() {
        miningDetector.radius = miningRange;
    }

    private void Update() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);
        if (hit2D.collider != null) {
            if (Input.GetMouseButtonDown(0)) {
                rsHit = hit2D.transform.gameObject;
                rsHit.GetComponent<Resource>().TakeDamage(miningHit);
                Stamina.Instance.ReduceStamina(staminaUsed);
            }
        }
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
