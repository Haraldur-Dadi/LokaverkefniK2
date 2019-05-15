using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory Instance;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int inventorySpace;

    public List<InventorySlot> inventory;

    private void Start() {
        foreach (InventorySlot slot in InventoryUI.Instance.slots) {
            inventory.Add(slot);
        }
    }

    public bool AddItem(Item item) {
        bool itemAdded = false;

        for (int i = 0; i < inventorySpace; i++) {
            if(inventory[i].item == item && inventory[i].item.stackAmount >= (inventory[i].itemStack + 1) && !itemAdded) {
                inventory[i].AddItem(item);
                itemAdded = true;
            } else if (inventory[i].item == null && !itemAdded) {
                inventory[i].AddItem(item);
                itemAdded = true;
            }
        }

        if (!itemAdded) {
            Debug.Log("Not enough room");
            return false;
        }

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(Item item) {
        item.slotEquipped.OnRemoveButton();

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
