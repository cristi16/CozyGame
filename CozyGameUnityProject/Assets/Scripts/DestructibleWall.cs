using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour, IProjectileHitListener {
    public float health = 100;

    public void OnProjectileHit(ProjectileHitInfo hitInfo)
    {
        health -= hitInfo.Damage;
    }
}
