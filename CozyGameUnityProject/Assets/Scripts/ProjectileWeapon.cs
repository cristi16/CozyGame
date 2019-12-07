using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public GameObject Projectile;
    public float FiringRate;
    public Vector3 MuzzlePosition;
    public int Ammunition;
    public int MaxAmmunition;
    public float ReloadDuration;

    private float lastShot;
    private float reloadStart = float.NaN;

    void FixedUpdate() {
        if (!float.IsNaN(reloadStart)) {
            if (Time.fixedTime >= reloadStart + ReloadDuration) {
                Ammunition = MaxAmmunition;
                reloadStart = float.NaN;
            }
        }
        if (float.IsNaN(reloadStart)) {
            IController controller = WeaponInventory == null ? null : WeaponInventory.GetComponent<IController>(); //TODO: This is a bit dodgy
            if (controller != null)
            {
                if (controller.Fire && Time.fixedTime >= lastShot + 1f / FiringRate) {
                    if (Ammunition > 0) {
                        GameObject projectile = Instantiate(Projectile, transform.position + transform.rotation * MuzzlePosition, transform.rotation);
                        projectile.GetComponent<Projectile>().Instigator = WeaponInventory.GetComponent<PlayerCharacterController>(); //TODO: This might be a bit dodgy
                        Ammunition--;
                    } else {
                        //TODO: Out-of-ammo click
                    }
                    lastShot = Time.fixedTime;
                } else if (controller.Reload && Ammunition < MaxAmmunition) {
                    reloadStart = Time.fixedTime;
                }
            }
        }
    }

    public interface IController {
        bool Fire { get; }
        bool Reload { get; }
    }
}
