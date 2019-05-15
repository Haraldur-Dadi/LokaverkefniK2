using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Rigidbody2D rb;

    public List<GameObject> detectedRange = new List<GameObject>();
    public List<string> priorityList;
    public GameObject target;

    public delegate void OnDetectedAdded();
    public OnDetectedAdded onDetectedAdded;

    public virtual void Start() {

    }

    public virtual void Update() {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D other) {
        //add building or player to list if in range
    }
    public virtual void OnTriggerExit2D(Collider2D other) {
        //remove item from list if out of range
    }

    public virtual void PickTarget() {
        //pick item with highest priority from detected list
    }

    public virtual void MoveToTarget() {
        //move towards target
    }

    public virtual void HitTarget() {
        //inflict damage on taget
    }

    public virtual void TakeDamage(float damage) {
        //take damage
    }
}
