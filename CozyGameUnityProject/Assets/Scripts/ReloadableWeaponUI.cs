using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadableWeaponUI : MonoBehaviour
{
    public PlayerCharacterController Player;
    public Sprite[] Sprites;

    public float FadeInDuration;
    public float FadeOutDuration;

    void Update() {
        Weapon weapon = Player.GetComponent<WeaponInventory>().Weapon;
        IReloadableWeapon reloadableWeapon = weapon == null ? null : weapon.GetComponent<IReloadableWeapon>();
        Image image = GetComponent<Image>();
        bool reloading = reloadableWeapon != null && reloadableWeapon.IsReloading;
        if (reloadableWeapon != null) {
            image.sprite = Sprites[reloadableWeapon.IsReloading ? (int) (reloadableWeapon.ReloadingProgress * (Sprites.Length - 1)) : Sprites.Length - 1];
        }
        image.color = new Color(1f, 1f, 1f,
            reloading
            ? Mathf.Min(image.color.a + Time.deltaTime / FadeInDuration, 1f)
            : Mathf.Max(image.color.a - Time.deltaTime / FadeOutDuration, 0f)
        );
    }
}
