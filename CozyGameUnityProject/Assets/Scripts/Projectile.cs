using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float Lifetime;
    public float Damage;
    public PlayerCharacterController Instigator;

    private float spawnTime;

    void Awake() {
        spawnTime = Time.fixedTime;
    }

    void FixedUpdate() {
        if (Time.fixedTime >= spawnTime + Lifetime) {
            Debug.Log("Projectile timed out.");
            Destroy(gameObject);
        }
        Speed += Time.fixedDeltaTime * Acceleration;
        if (Speed <= 0f) {
            Debug.Log("Projectile speed dropped below 0.");
            Destroy(gameObject);
        }
        float distance = Time.fixedDeltaTime * Speed;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, distance, ~LayerMask.GetMask("Players"))) {
            transform.position = hit.point;
            ProjectileHitInfo hitInfo = new ProjectileHitInfo()
            {
                Damage = Damage,
                Instigator = Instigator
            };
            IProjectileHitListener[] hitListeners = hit.collider.GetComponents<IProjectileHitListener>();
            foreach(IProjectileHitListener listener in hitListeners)
            {
                listener.OnProjectileHit(hitInfo);
            }
            Destroy(gameObject);
        } else {
            transform.Translate(distance * Vector3.forward);
        }
    }
}
