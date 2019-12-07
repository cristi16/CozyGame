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
    public IController Controller = null;

    private float lastShot;

    void FixedUpdate() {
        if (Controller != null && Controller.Fire && Time.fixedTime >= lastShot + 1f / FiringRate) {
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
