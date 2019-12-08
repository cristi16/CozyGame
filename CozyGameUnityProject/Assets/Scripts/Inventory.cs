using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Item[] Items {
        get {
            return GetComponentsInChildren<Item>();
        }
    }
    public void Equip(Item item) {
        if (item.Inventory != this) {
            
        }
    }
}
