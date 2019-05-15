using UnityEngine;

public class Forge : MonoBehaviour {

    public GameObject craftingUI;
    public GameObject keyBind;

    public bool playerNear = false;

    private void Start() {
        keyBind.SetActive(false);
        craftingUI.SetActive(false);
    }

    private void Update() {
        if (playerNear && Input.GetButtonDown("Use")) {
            craftingUI.SetActive(!craftingUI.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            keyBind.SetActive(true);
            playerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerNear = false;
            keyBind.SetActive(false);
            craftingUI.SetActive(false);
        }
    }

}
