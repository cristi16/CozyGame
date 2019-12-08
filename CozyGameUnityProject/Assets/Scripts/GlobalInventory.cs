using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventory : MonoBehaviour {
    private static GlobalInventory instance;

    public static GlobalInventory Instance
    {
        get
        {
            if (instance == null) {
                throw new System.Exception("Inventory not initialized.");
            }
            return instance;
        }
        set
        {
            if (instance != null && value != instance) {
                Destroy(instance);
                instance = value;
            }
        }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            throw new System.Exception("Multiple Inventory instances found.");
        }
    }

    private ICollection<string> items = new List<string>();

    public void AddItem(string item) {
        items.Add(item);
    }

    public bool RemoveItem(string item) {
        return items.Remove(item);
    }

    public bool HasItem(string item) {
        return items.Contains(item);
    }

    public bool HasItems(string[] items) {
        for (int i = 0; i < items.Length; i++)
            if (!this.items.Contains(items[i]))
                return false;
        return true;
    }
}
