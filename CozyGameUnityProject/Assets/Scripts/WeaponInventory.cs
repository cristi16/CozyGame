using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public float PickupRange;
    public Weapon Weapon;

    void Awake() {
        Weapon = GetComponentInChildren<Weapon>();
    }

    void FixedUpdate() {
         IController controller = GetComponent<IController>();
        if (controller != null) {
            Weapon weapon = controller.PickUpWeapon;
            if (weapon != null && (weapon.transform.position - transform.position).magnitude <= PickupRange) {
                Equip(weapon);
            }
            if (controller.DropWeapon) {
                Unequip();
            }
        }
    }

    public void Equip(Weapon weapon) {
        if (weapon != Weapon) {
            if (Weapon != null) {
                Weapon.transform.parent = transform.parent;
                Weapon.SpriteRenderer.enabled = true;
            }
            Weapon = weapon;
            weapon.SpriteRenderer.enabled = false;
            weapon.transform.parent = transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    public void Unequip() {
        if (Weapon != null) {
            Weapon.transform.parent = transform.parent;
            Weapon.SpriteRenderer.enabled = true;
            Weapon = null;
        }
    }

    public interface IController {
        bool DropWeapon{ get; }
        Weapon PickUpWeapon { get; }
    }
}
