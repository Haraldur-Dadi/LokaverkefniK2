using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {

    public int healthIncrease;
    public int staminaIncrease;

    public override void Use() {
        base.Use();

        if(healthIncrease > 0) {
            Health.Instance.AddHealth(healthIncrease);
        }
        if(staminaIncrease > 0) {
            Stamina.Instance.AddStamina(staminaIncrease);
        }

    }
}
