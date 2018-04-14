using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IBulletHitListener {
    public float health;

    public void OnBulletHit(BulletHitInfo hitInfo)
    {
        health -= hitInfo.damage;
        if(health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
