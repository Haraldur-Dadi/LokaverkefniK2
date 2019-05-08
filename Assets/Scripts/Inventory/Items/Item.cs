using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public ItemType itemType;
    public ItemRarity itemRarity;
    public ItemLooted itemLooted;
    public ItemUse itemUse;

    public virtual void Add() {
        Inventory.Instance.AddItem(this);
    }

    public void RemoveFromInventory() {
        Inventory.Instance.RemoveItem(this);
    }

    public virtual void Use() {

    }
}

public enum ItemType { Weapon, Armor, HealthReg, ManaReg, Material, Backpack }
public enum ItemRarity {
    Poor, // #9d9d9d
    Common, // #ffffff
    Uncommon, // #1eff00
    Rare, // #0080ff
    Epic, // #b048f8
    Legnedary, // #ff8000
}
public enum ItemLooted { Starter, Quest, Mobs, Chest }
public enum ItemUse { Consume, Place, Equip, Discard }