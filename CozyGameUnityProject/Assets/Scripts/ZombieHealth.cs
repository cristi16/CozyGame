using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IBulletHitListener, IExplosionHitListener {
    public float health;

    public void OnBulletHit(BulletHitInfo hitInfo)
    {
        health -= hitInfo.damage;
		hitInfo.instigator.damageInflicted+=hitInfo.damage;
        if(health <= 0.0f)
        {
			//Zombie Dies
			hitInfo.instigator.killCount++;
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
