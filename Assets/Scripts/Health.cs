using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour {

    public static Health Instance;

    public bool dead;

    public float health;
    public float maxHealth = 100;

    public Slider hpBar;
    public TextMeshProUGUI hpPc;

    public GameObject deathScreen;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start() {
        dead = false;
        deathScreen.SetActive(false);
        health = maxHealth;
        CalculateHpBar();
    }
    private void Update() {
        if (dead) {
            GameManager.isPaused = true;
        }
    }
    public void TakeDamage(float damage) {
        if(!dead) {
            health -= damage;
            CalculateHpBar();
            if(health <= 0) {
                Die();
            }
        }
    }

    public void AddHealth(float healthToAdd) {
        if((health+healthToAdd) >= maxHealth) {
            health = maxHealth;
        } else {
            health += healthToAdd;
        }
        CalculateHpBar();
    }

    void CalculateHpBar() {
        hpBar.value = health / maxHealth;
        hpPc.text = (hpBar.value * 100).ToString() + "%";
    }

    void Die() {
        dead = true;
        deathScreen.SetActive(true);
    }
}
