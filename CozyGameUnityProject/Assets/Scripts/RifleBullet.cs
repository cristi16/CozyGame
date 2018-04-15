using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour {

    [HideInInspector] public float velocity;
    [HideInInspector] public int damage;
    [HideInInspector] public PlayerCharacterController instigator; 

    void Update()
    {
        //
        Vector3 previousPosition = transform.position;
        float moveDistance = velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(new Ray(previousPosition, transform.forward), out hit, moveDistance, ~LayerMask.GetMask("Players")))
        {
            transform.position = hit.point;

            // TODO: Impact effect

            BulletHitInfo hitInfo = new BulletHitInfo()
            {
                damage = damage,
                instigator = instigator
            };
            var hitListeners = hit.collider.GetComponents<IBulletHitListener>();
            foreach(var listener in hitListeners)
            {
                listener.OnBulletHit(hitInfo);
            }

            // TODO: deal damage
            Destroy(gameObject);
        }
        else
        {
            transform.position = previousPosition + transform.forward * moveDistance;
        }
    }

	public static void Spawn(GameObject prefab, float timeToLive, Vector3 muzzlePosition, Quaternion muzzleRotation, float muzzleVelocity, int damage, PlayerCharacterController instigator)
    {
        GameObject bullet = Instantiate(prefab, muzzlePosition, muzzleRotation);
        var bulletMovement = bullet.AddComponent<RifleBullet>();
        bulletMovement.velocity = muzzleVelocity;
        bulletMovement.damage = damage;
		bulletMovement.instigator = instigator;
        Destroy(bullet, timeToLive);
    }
}
