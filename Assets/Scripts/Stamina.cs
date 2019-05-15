using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Stamina : MonoBehaviour {

    public static Stamina Instance;

    public bool startRecovery;
    public bool resting;

    public float maxStamina = 100;
    public float stamina;
    public float staminaRegen = 2;
    public float timeBtwStaminaRegen = 3;

    public Slider staminaBar;
    public TextMeshProUGUI staminaPc;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        stamina = maxStamina;
        CalculateStaminaBar();
        resting = true;
        startRecovery = true;
    }
    private void Update() {
        if (startRecovery) {
            StartCoroutine(GainStamina());
            Debug.Log("Starting recovery");
        }
    }

    public void ReduceStamina(float staminaUsed) {
        stamina -= staminaUsed;
        CalculateStaminaBar();
    }
    public void AddStamina(float staminaToAdd) {
        if((stamina + staminaToAdd) >= maxStamina) {
            stamina = maxStamina;
        } else {
            stamina += staminaToAdd;
        }
        CalculateStaminaBar();
    }

    void CalculateStaminaBar() {
        staminaBar.value = stamina / maxStamina;
        staminaPc.text = (staminaBar.value * 100).ToString() + "%";
    }

    IEnumerator GainStamina() {
        while (resting) {
            startRecovery = false;
            if (stamina < maxStamina) {
                stamina += staminaRegen;
                CalculateStaminaBar();
            }

            yield return new WaitForSeconds(timeBtwStaminaRegen);
        }
        if(!resting) {
            Debug.Log("Stopping regen");
        }
    }
}
