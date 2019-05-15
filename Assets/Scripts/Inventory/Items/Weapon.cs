using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item {

    public WeaponSlotEnum equipSlot;
    // public WeaponType weaponType;
    public int itemLvl;
    public Sprite weaponObject;

    public int damage;
    public float attackSpeed;

    public int strength;
    public int vitality;
    public int dexterity;
    public int focus;

    public bool twoHanded;
    public AudioClip weaponSoundEffect;

    public override void Use() {
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum WeaponSlotEnum { MainWeapon, SecondaryWeapon, Pickaxe }
// public enum WeaponType { Axe, Dagger, Sword, Staff, Bow, Wand, Shield }