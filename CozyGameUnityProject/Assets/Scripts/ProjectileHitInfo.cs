using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileHitInfo
{
    public PlayerCharacterController Instigator;
    public float Damage;
}

public interface IProjectileHitListener {
    void OnProjectileHit(ProjectileHitInfo hit);
}
