using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FadeObjectInOut))]
public class Foreground : MonoBehaviour {
    private bool visible = true;
    private const float ratio = 2f;
    private int heroCount = 0;
    private FadeObjectInOut fade;

    void Awake() {
        fade = GetComponent<FadeObjectInOut>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerCharacterController>() != null && ++heroCount > 0) fade.FadeOut();
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerCharacterController>() != null && --heroCount == 0) fade.FadeIn();
    }
}
