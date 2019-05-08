using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    Item item;

    public Image icon;
    public Button whatToDoBT;
    public GameObject whatToDoGO;

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        whatToDoBT.interactable = true;
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        whatToDoBT.interactable = false;
    }

    public void WhatToDo() {
        whatToDoGO.SetActive(true);
    }

    public void OnRemoveButton() {
        Inventory.Instance.RemoveItem(item);
        whatToDoGO.SetActive(false);
    }

    public void EquipItem() {
        item.Use();
        whatToDoGO.SetActive(false);
    }

    public void UseItem() {
        Debug.Log("Placeholder for when we add consumables");
        whatToDoGO.SetActive(false);
    }
}
