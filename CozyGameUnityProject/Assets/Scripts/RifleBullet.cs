using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour {
    [HideInInspector] public float velocity;
    void Update()
    {
        transform.position = transform.position + transform.forward * (velocity * Time.deltaTime);
    }
}
