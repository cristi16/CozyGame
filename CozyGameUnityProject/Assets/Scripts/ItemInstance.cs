using UnityEngine;
using System.Collections;

public class ItemInstance : MonoBehaviour {
    public string Item;

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerCharacterController>() != null) {
            GlobalInventory.Instance.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
