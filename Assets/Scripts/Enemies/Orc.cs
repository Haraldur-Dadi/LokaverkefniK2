using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orc : Enemy {

    public float speed = 1.5f;

    public float maxHp;
    public float hp;
    public float timeSinceLastHit;
    public Slider hpBar;

    public float damage = 5;
    public float attackSpeed = 2;
    public float attackRange = .2f;
    public LayerMask layerMask;

    public GameObject hitFx;
    public GameObject backgroundHitFx;

    public override void Start() {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
        hpBar.gameObject.SetActive(false);
    }

    public override void Update() {
        base.Update();
        if (target == null) {
            PickTarget();
        } else {
            Vector2 rayDirection = target.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, layerMask);
            if (hit) {
                if (hit.distance >= attackRange && hit.collider.CompareTag("Player")) {
                    MoveToTarget();
                } else if (hit.distance < attackRange && hit.collider.CompareTag("Player")) {
                    timeSinceLastHit -= GameManager.DeltaTime;
                    rb.isKinematic = true;
                    if (attackSpeed <= 0) {
                        HitTarget();
                    }
                }
            }
        }

        attackSpeed -= GameManager.DeltaTime;
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (!detectedRange.Contains(other.gameObject)) {
            if (other.CompareTag("Player")) {
                detectedRange.Add(other.gameObject);
            }
        }
    }

    public override void PickTarget() {
        base.PickTarget();
        List<GameObject> targetList = new List<GameObject>();
        GameObject tempTarget = null;
        foreach (string priorityLvl in priorityList) {
            foreach (GameObject possibleTarget in detectedRange) {
                if (possibleTarget.CompareTag(priorityLvl)) {
                    targetList.Add(possibleTarget.gameObject);
                }
            }
        }
        float minDist = Mathf.Infinity;
        foreach (GameObject targets in targetList) {
            float dist = Vector2.Distance(gameObject.transform.position, targets.transform.position);
            if(dist < minDist) {
                minDist = dist;
                tempTarget = targets;
            }
        }
        if(tempTarget != null) {
            target = tempTarget;
        }
    }
    public override void MoveToTarget() {
        base.MoveToTarget();
        rb.MovePosition(Vector2.MoveTowards(transform.position, target.transform.position, speed * GameManager.DeltaTime));
    }
    public override void HitTarget() {
        base.HitTarget();
        if (target.CompareTag("Player")) {
            attackSpeed = 2;
            target.GetComponent<Health>().TakeDamage(damage);
        }
    }
    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        if (hp > 1) {
            timeSinceLastHit = 3;
            hp -= damage;
            CalculateHpBar();
            if (hitFx != null) {
                Instantiate(hitFx, transform.position + hitFx.transform.position, Quaternion.identity, transform);
            }
            if (backgroundHitFx != null) {
                Instantiate(backgroundHitFx, transform.position + backgroundHitFx.transform.position, Quaternion.identity, transform);
            }
            MainCamera.Instance.CameraShake();
        } else {
            Die();
        }

    }
    public void CalculateHpBar() {
        hpBar.gameObject.SetActive(true);
        if (timeSinceLastHit > 0) {
            hpBar.value = hp / maxHp;
        } else {
            hpBar.gameObject.SetActive(false);
        }
    }
    public void Die() {
        WaveSpawner.EnemiesAlive -= 1;
        Destroy(gameObject);
    }
}
