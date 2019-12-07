using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponInventory WeaponInventory {
        get {
            return transform.parent.GetComponent<WeaponInventory>();
        }
    }

    void Awake() {
        Equipped = WeaponInventory != null;
    }
}
