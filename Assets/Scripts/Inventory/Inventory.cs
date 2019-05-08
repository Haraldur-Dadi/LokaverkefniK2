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

    public List<Item> inventory = new List<Item>();

    public bool AddItem(Item item) {
        if (inventory.Count >= inventorySpace) {
            Debug.Log("Not enough room");
            return false;
        }
        inventory.Add(item);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(Item item) {
        inventory.Remove(item);

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
