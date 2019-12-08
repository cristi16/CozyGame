using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite Icon;
    public SpriteRenderer SpriteRenderer;

    public Inventory Inventory {
        get {
            return transform.parent.GetComponent<Inventory>();
        }
    }

    public bool Equipped {
        get {
            return !SpriteRenderer.enabled;
        }
        set {
            SpriteRenderer.enabled = !value;
        }
    }

    void Awake() {
        Equipped = Inventory != null;
    }
}
