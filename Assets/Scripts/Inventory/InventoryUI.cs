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
            if (i < Inventory.Instance.inventory.Count)
            {
                slots[i].AddItem(Inventory.Instance.inventory[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
