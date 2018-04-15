using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {
    public BaseWeapon weaponPrefab;
    public Color pulseAColor;
    public Color pulseBColor;
    public SpriteRenderer previewSprite;

    void OnTriggerEnter(Collider other)
    {
        var playerCharacter = other.GetComponent<PlayerCharacterController>();
        if(playerCharacter != null)
        {
            playerCharacter.SetCurrentWeaponSlot(weaponPrefab);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        previewSprite.color = Color.Lerp(pulseAColor, pulseBColor, Mathf.PingPong(Time.time, 1.0f));
    }
}
