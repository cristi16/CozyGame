using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponInventory WeaponInventory {
        get {
            Transform parent = transform.parent;
            return parent == null ? null : parent.GetComponent<WeaponInventory>();
        }
    }

    void Awake() {
        Equipped = WeaponInventory != null;
    }
}
