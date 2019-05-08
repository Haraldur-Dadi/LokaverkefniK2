using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public static EquipmentManager Instance;

    public Armor[] currentArmor;
    public Weapon[] currentWeapon;
    public Backpack[] currentBackpacks;
    public int backpackSlots = 5;

    public Armor[] starterArmor;
    public Weapon[] starterWeapon;
    public Backpack[] starterBackpack;

    public delegate void OnArmorChanged(Armor newItem, Armor oldItem);
    public OnArmorChanged onArmorChanged;

    public delegate void OnWeaponChanged(Weapon newItem, Weapon oldItem);
    public OnWeaponChanged onWeaponChanged;

    public delegate void OnBackpackChanged(Backpack newItem, Backpack oldItem);
    public OnBackpackChanged onBackpackChanged;

    void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    private void Start() {
        int numArmorSlots = System.Enum.GetNames(typeof(ArmorSlotEnum)).Length;
        int numWeaponSlots = System.Enum.GetNames(typeof(WeaponSlotEnum)).Length;
        currentArmor = new Armor[numArmorSlots];
        currentWeapon = new Weapon[numWeaponSlots];
        currentBackpacks = new Backpack[backpackSlots];

        foreach (Armor armor in starterArmor) {
            Equip(armor);
        }
        foreach (Weapon weapon in starterWeapon) {
            Equip(weapon);
        }
        foreach (Backpack backpack in starterBackpack) {
            Equip(backpack);
        }
    }

    public void Equip(Item newItem) {

        if (newItem is Armor) {
            var NewArmor = newItem as Armor;
            int slotIndex = (int)NewArmor.equipSlot;
            Armor oldItem = null;

            if (currentArmor[slotIndex] != null) {
                oldItem = currentArmor[slotIndex];
                Inventory.Instance.AddItem(oldItem);
            }

            if (onArmorChanged != null) {
                onArmorChanged.Invoke(NewArmor, oldItem);
            }

            currentArmor[slotIndex] = NewArmor;
        } 
        if (newItem is Weapon) {
            var NewWeapon = newItem as Weapon;
            int slotIndex = (int)NewWeapon.equipSlot;
            Weapon oldItem = null;

            if (currentWeapon[slotIndex] != null) {
                oldItem = currentWeapon[slotIndex];
                Inventory.Instance.AddItem(oldItem);
            }

            if (onWeaponChanged != null) {
                onWeaponChanged.Invoke(NewWeapon, oldItem);
            }

            currentWeapon[slotIndex] = NewWeapon;
        }
        if (newItem is Backpack) {
            var NewBackpack = newItem as Backpack;
            Backpack oldItem = null;

            int counter = 0;
            foreach (Backpack backpack in currentBackpacks) {
                if (backpack != null) {
                    counter += 1;
                } else {
                    currentBackpacks[counter] = NewBackpack;
                    if (onBackpackChanged != null) {
                        onBackpackChanged.Invoke(NewBackpack, oldItem);
                    }
                    UpdateInventorySpace();
                    break;
                }
            }
        }
    }

    public void UnequipArmor(int slotIndex) {
        if (currentArmor[slotIndex] != null) {
            Armor oldItem = currentArmor[slotIndex];
            Inventory.Instance.AddItem(oldItem);

            currentArmor[slotIndex] = null;

            if (onArmorChanged != null) {
                onArmorChanged.Invoke(null, oldItem);
            }
        }
    }
    public void UnequipWeapon(int slotIndex) {
        if (currentWeapon[slotIndex] != null) {
            Weapon oldItem = currentWeapon[slotIndex];
            Inventory.Instance.AddItem(oldItem);

            currentWeapon[slotIndex] = null;

            if (onWeaponChanged != null) {
                onWeaponChanged.Invoke(null, oldItem);
            }
        }
    }
    public void UnequipBackpack(int slotIndex) {
        if (currentBackpacks[slotIndex] != null) {
            Backpack oldItem = currentBackpacks[slotIndex];
            Inventory.Instance.AddItem(oldItem);

            currentBackpacks[slotIndex] = null;

            if (onBackpackChanged != null) {
                onBackpackChanged.Invoke(null, oldItem);
            }
            UpdateInventorySpace();
        }
    }

    public void UpdateInventorySpace() {
        Debug.Log("Hello");
        int inventorySlots = 0;
        foreach (Backpack backpack in currentBackpacks) {
            if(backpack != null) {
                inventorySlots += backpack.slots;
            }
        }
        Inventory.Instance.inventorySpace = inventorySlots;
        InventoryUI.Instance.UpdateNumberOfSlots();
    }
}
