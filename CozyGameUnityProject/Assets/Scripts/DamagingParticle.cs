using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingParticle : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float Lifetime;
    public float DamageRate;
    public float GrowthRate;
    public PlayerCharacterController Instigator;

    private float spawnTime;

    void Awake() {
        spawnTime = Time.fixedTime;
    }

    void FixedUpdate() {
        if (Time.fixedTime >= spawnTime + Lifetime) {
            Debug.Log("DamagingParticle timed out.");
            Destroy(gameObject);
        }
        Speed += Time.fixedDeltaTime * Acceleration;
        if (Speed <= 0f) {
            Debug.Log("DamagingParticle speed dropped below 0.");
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(
            transform.localScale.x + Time.fixedDeltaTime * GrowthRate,
            transform.localScale.y + Time.fixedDeltaTime * GrowthRate,
            transform.localScale.z + Time.fixedDeltaTime * GrowthRate
        );
        transform.Translate(Time.fixedDeltaTime * Speed * Vector3.forward);
    }
}
