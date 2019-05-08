using UnityEngine;

public class MotherUI : MonoBehaviour {

    public GameObject motherUI;
    public GameObject gear;
    public GameObject inventory;
    public GameObject quest;
    public GameObject map;
    public GameObject settings;

    void Start() {
        Gear();
        motherUI.SetActive(false);
    }

    void Update() {
        if (Input.GetButtonDown("MotherUI")) {
            motherUI.SetActive(!motherUI.activeSelf);
            CanMove();
        }
        if (Input.GetButtonDown("Gear")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Gear();
            CanMove();
        }
        if (Input.GetButtonDown("Inventory")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Inventory();
            CanMove();
        }
        if (Input.GetButtonDown("Quest")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Quest();
            CanMove();
        }
        if(Input.GetButtonDown("Map")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Map();
            CanMove();
        }
        if (Input.GetButtonDown("Settings")) {
            motherUI.SetActive(!motherUI.activeSelf);
            Settings();
            CanMove();
        }
    }

    void CanMove() {
        if (motherUI.activeSelf == true) {
            Movement.Instance.canMove = false;
        } else {
            Movement.Instance.canMove = true;
        }
    }

    public void Gear() {
        gear.SetActive(true);
        inventory.SetActive(false);
        quest.SetActive(false);
        map.SetActive(false);
        settings.SetActive(false);
    }
    public void Inventory() {
        gear.SetActive(false);
        inventory.SetActive(true);
        quest.SetActive(false);
        map.SetActive(false);
        settings.SetActive(false);
    }
    public void Quest() {
        gear.SetActive(false);
        inventory.SetActive(false);
        quest.SetActive(true);
        map.SetActive(false);
        settings.SetActive(false);
    }
    public void Map() {
        gear.SetActive(false);
        inventory.SetActive(false);
        quest.SetActive(false);
        map.SetActive(true);
        settings.SetActive(false);
    }
    public void Settings() {
        gear.SetActive(false);
        inventory.SetActive(false);
        quest.SetActive(false);
        map.SetActive(false);
        settings.SetActive(true);
    }
}
