using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {

    public Item item;
    public int itemStack;
    public TextMeshProUGUI itemStackTxt;

    public Image icon;
    public Button whatToDoBT;
    public GameObject equipButton;
    public GameObject useButton;
    public GameObject whatToDoGO;

    public void AddItem(Item newItem) {
        if(item == null) {
            item = newItem;
            item.slotEquipped = this;

            if (item.itemUse == ItemUse.Equip) {
                equipButton.SetActive(true);
            } else {
                equipButton.SetActive(false);
            }

            if (item.itemUse == ItemUse.Consume) {
                useButton.SetActive(true);
                useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            } else if (item.itemUse == ItemUse.Place) {
                useButton.SetActive(true);
                useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Place";
            } else {
                useButton.SetActive(false);
            }

            icon.sprite = item.icon;
            icon.enabled = true;
            whatToDoBT.interactable = true;
        } else {
            itemStack += 1;
        }

    }

    public void ClearSlot() {
        item = null;
        itemStack = 0;

        icon.sprite = null;
        icon.enabled = false;
        whatToDoBT.interactable = false;
    }

    public void WhatToDo() {
        whatToDoGO.SetActive(!whatToDoGO.activeSelf);
    }

    public void OnRemoveButton() {
        if(itemStack > 1) {
            itemStack -= 1;
        } else {
            ClearSlot();
        }
        if (Inventory.Instance.onItemChangedCallback != null) {
            Inventory.Instance.onItemChangedCallback.Invoke();
        }
        whatToDoGO.SetActive(false);
    }

    public void EquipItem() {
        item.Use();
        whatToDoGO.SetActive(false);
        if (Inventory.Instance.onItemChangedCallback != null) {
            Inventory.Instance.onItemChangedCallback.Invoke();
        }
    }

    public void UseItem() {
        if(item.itemUse == ItemUse.Consume) {
            item.Use();
            OnRemoveButton();
        }
    }
}
