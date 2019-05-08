using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Item {

    public ArmorSlotEnum equipSlot;
    public ArmorType armorType;
    public ArmorSet armorSet;

    public int itemLvl;
    public int armor;

    public int strength;
    public int vitality;
    public int dexterity;
    public int focus;

    public override void Use() {
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum ArmorSlotEnum { Head, Chest, Hands, Belt, Legs, Feet}
public enum ArmorType { Cloth, Leather, Mail, Plate , Necklace, Ring, Trinket }
public enum ArmorSet { starter }