using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite Icon;

    public Inventory Container {
        get {
            return transform.parent.GetComponent<Inventory>();
        }
    }

    public bool Equipped {
        get {
            return !GetComponent<SpriteRenderer>().enabled;
        }
        set {
            GetComponent<SpriteRenderer>().enabled = !value;
        }
    }

    void Awake() {
        Equipped = Container != null;
    }
}
