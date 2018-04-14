using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour, IBulletHitListener {
    public float health = 100;

    public void OnBulletHit(BulletHitInfo hitInfo)
    {
        health -= hitInfo.damage;
    }
}
