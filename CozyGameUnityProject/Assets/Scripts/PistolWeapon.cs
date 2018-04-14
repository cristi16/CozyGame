using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : BaseWeapon{
    public float maxRoundsPerMinute = 45.0f;
    public GameObject bulletPrefab;
    public float muzzleVelocity = 25.0f;
    public float muzzleDistance = 0.6f;
    public float bulletMaxTimeAlive = 3.0f;

    private float m_LastShotFired;

    public override void StartFiring()
    {
        if(m_LastShotFired + 60.0f / maxRoundsPerMinute >= Time.time)
        {
            return;
        }

        RifleBullet.Spawn(bulletPrefab, bulletMaxTimeAlive, transform.position + transform.forward * muzzleDistance, transform.rotation, muzzleVelocity);
        m_LastShotFired = Time.time;
    }

    public override void StopFiring() { }
}
