using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float Lifetime;

    private float spawnTime;

    public void Awake() {
        spawnTime = Time.fixedTime;
    }

    void FixedUpdate() {
        if (Time.fixedTime >= spawnTime + Lifetime) {
            Destroy(gameObject);
        }
        Speed += Time.deltaTime * Acceleration;
        if (Speed <= 0f) {
            Destroy(gameObject);
        }
        transform.Translate(Time.deltaTime * Speed * Vector3.right);
    }
}
