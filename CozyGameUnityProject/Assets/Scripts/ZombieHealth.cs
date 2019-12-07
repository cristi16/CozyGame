using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IProjectileHitListener, IExplosionHitListener {
    public float health;

    public void OnProjectileHit(ProjectileHitInfo hitInfo)
    {
        health -= hitInfo.Damage;
		hitInfo.Instigator.damageInflicted+=hitInfo.Damage;
        if(health <= 0.0f)
        {
			//Zombie Dies
			hitInfo.Instigator.killCount++;
            Destroy(gameObject);
        }
    }

    public void OnExplosionHit(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            //Zombie Dies
            Destroy(gameObject);
        }
    }
}
