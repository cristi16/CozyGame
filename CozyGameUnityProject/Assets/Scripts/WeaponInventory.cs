using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public Weapon Weapon;

    void Awake() {
        Weapon = GetComponentInChildren<Weapon>();
    }

    public void Equip(Weapon weapon) {
        if (weapon != Weapon) {
            if (Weapon != null) {
                Weapon.transform.parent = transform.parent;
            }
            Weapon = weapon;
            weapon.transform.parent = transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    public void Unequip() {
        Weapon.transform.parent = transform.parent;
        Weapon = null;
    }
}
