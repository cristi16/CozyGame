using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletHitInfo
{
    public PlayerCharacterController instigator;
    public int damage;
}

public interface IBulletHitListener {
    void OnBulletHit(BulletHitInfo hit);
}
