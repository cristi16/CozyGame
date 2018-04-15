using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gate : MonoBehaviour {
    public string[] RequiredItems;

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerCharacterController>() != null) {
            if (Inventory.Instance.HasItems(RequiredItems)) {
                GetComponent<Animator>().SetTrigger("Open");
                Destroy(this);
            } else {
                GetComponent<Animator>().SetTrigger("OpenFail");
            }
        }
    }
}
