using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : MonoBehaviour {
    public float maxRoundsPerMinute = 45.0f;
    public GameObject bulletPrefab;
    public float muzzleVelocity = 25.0f;
    public float bulletMaxTimeAlive = 3.0f;

    private float m_LastShotFired;

    public bool Fire()
    {
        if(m_LastShotFired + 60.0f / maxRoundsPerMinute >= Time.time)
        {
            return false;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        var bulletMovement = bullet.AddComponent<RifleBullet>();
        bullet.GetComponent<TrailRenderer>().enabled = true;
        bulletMovement.velocity = muzzleVelocity;
        Destroy(bullet, bulletMaxTimeAlive);
        m_LastShotFired = Time.time;
        return true;
    }
}
