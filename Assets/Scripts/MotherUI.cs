using UnityEngine;

public class MotherUI : MonoBehaviour {

    public GameObject motherUI;
    public GameObject gear;
    public GameObject inventory;
    public GameObject quest;
    public GameObject settings;

    void Start() {
        motherUI.SetActive(false);
    }

    void Update() {
        if (Input.GetButtonDown("MotherUI")) {
            motherUI.SetActive(!motherUI.activeSelf);
        }
        if (Input.GetButtonDown("Gear")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Gear();
        }
        if (Input.GetButtonDown("Inventory")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Inventory();
        }
        if (Input.GetButtonDown("Quest")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Quest();
        }
        if (Input.GetButtonDown("Settings")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Settings();
        }
    }
    public void Gear() {
        gear.SetActive(true);
        inventory.SetActive(false);
        quest.SetActive(false);
        settings.SetActive(false);
    }
    public void Inventory() {
        gear.SetActive(false);
        inventory.SetActive(true);
        quest.SetActive(false);
        settings.SetActive(false);
    }
    public void Quest() {
        gear.SetActive(false);
        inventory.SetActive(false);
        quest.SetActive(true);
        settings.SetActive(false);
    }
    public void Settings() {
        gear.SetActive(false);
        inventory.SetActive(false);
        quest.SetActive(false);
        settings.SetActive(true);
    }
}
