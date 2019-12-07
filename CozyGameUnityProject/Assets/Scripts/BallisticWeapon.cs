using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticWeapon : Weapon
{
    public GameObject Projectile;
    public float FiringRate;
    public float MuzzleDistance;
    public int Ammunition;
    public int MaxAmmunition;

    private float lastShot;

    void FixedUpdate() {
        IController controller = Container == null ? null : Container.GetComponent<IController>(); //TODO: This is a bit dodgy
        if (controller != null && controller.Fire && Time.fixedTime >= lastShot + 1f / FiringRate) {
            if (Ammunition > 0) {
                Instantiate(Projectile, transform.position + MuzzleDistance * Vector3.right, transform.rotation);
                Ammunition--;
            } else {
                //TODO: Out-of-ammo click
            }
            lastShot = Time.fixedTime;
        }
    }

    public interface IController {
        bool Fire { get; }
    }
}
