using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IBulletHitListener, IExplosionHitListener {
    public float health;
    float maxHealth;

    Material m;
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
