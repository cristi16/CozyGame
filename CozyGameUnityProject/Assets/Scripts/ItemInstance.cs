using UnityEngine;
using System.Collections;

public class ItemInstance : MonoBehaviour {
    public string Item;

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerCharacterController>() != null) {
            Inventory.Instance.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
