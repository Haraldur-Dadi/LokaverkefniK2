using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public static InventoryUI Instance;

    public Transform itemsParent;

    public InventorySlot[] slots;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
    void Start() {
        Inventory.Instance.onItemChangedCallback += UpdateUI;
        UpdateUI();
    }

    public void UpdateNumberOfSlots() {
        for (int i = 0; i < slots.Length; i++) {
            if(i < Inventory.Instance.inventorySpace) {
                slots[i].gameObject.SetActive(true);
            } else {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateUI() {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++) {
            if(slots[i].itemStack > 1) {
                slots[i].itemStackTxt.text = slots[i].itemStack.ToString();
            } else {
                slots[i].itemStackTxt.text = "";
            }

            if (slots[i].item == null) {
                slots[i].ClearSlot();
            }
        }
    }
}
