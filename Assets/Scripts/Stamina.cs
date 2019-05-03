using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stamina : MonoBehaviour {

    public static Stamina Instance;

    public bool startRecovery;
    public bool resting;

    public float maxStamina = 100;
    public float stamina;
    public float staminaRegen = 2;
    public float timeBtwStaminaRegen = 3;
    public Slider staminaBar;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        stamina = maxStamina;
        staminaBar.value = stamina / maxStamina;
        startRecovery = true;
        resting = true;
    }
    private void Update() {
        if (startRecovery) {
            StartCoroutine(GainStamina());
            Debug.Log("Starting recovery");
        }
    }

    public void ReduceStamina(float staminaUsed) {
        stamina -= staminaUsed;
        StaminaBar();
    }

    void StaminaBar() {
        staminaBar.value = stamina / maxStamina;
    }

    IEnumerator GainStamina() {
        while (resting) {
            startRecovery = false;
            if (stamina < maxStamina) {
                stamina += staminaRegen;
                StaminaBar();
            }

            yield return new WaitForSeconds(timeBtwStaminaRegen);
        }
        if(!resting) {
            Debug.Log("Stopping regen");
        }
    }
}
