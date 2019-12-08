using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] Sprites;

    void Awake() {
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length - 1)];
    }
}
