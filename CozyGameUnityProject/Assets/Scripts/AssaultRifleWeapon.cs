using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleWeapon : BaseWeapon{
    public float roundsPerMinute = 120.0f;

    public float muzzleOffset = 0.75f;
    public float muzzleVelocity = 20.0f;
    public GameObject bulletPrefab = null;
    public float bulletTimeToLive;
    public int damagePerRound = 10;

    private float m_LastBulletFired = -1000.0f;

    public override void StartFiring()
    {
        enabled = true;
    }

    public override void StopFiring()
    {
        enabled = false;
    }

    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        if(m_LastBulletFired + 60.0f / roundsPerMinute < Time.time)
        {
			RifleBullet.Spawn(bulletPrefab, bulletTimeToLive, transform.position + transform.forward * muzzleOffset, transform.rotation, muzzleVelocity, damagePerRound, instigator);
            m_LastBulletFired = Time.time;
        }
    }
}
