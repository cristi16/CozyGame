using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IProjectileHitListener, IExplosionHitListener {
    public float health;
	float maxHealth;

    Material m;

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

    public void Start()
    {
        m = GetComponentInChildren<SpriteRenderer>().material;
        maxHealth = health;
    }

    public void Update()
    {        
        Color c = Color.Lerp(Color.white, Color.red, 1 - health / maxHealth);
        m.color = c;
    }
}
