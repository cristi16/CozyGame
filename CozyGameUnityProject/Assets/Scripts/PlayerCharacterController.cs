﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour,
IGenerateNoise,
IExplosionHitListener,
ProjectileWeapon.IController,
WeaponInventory.IController
{
    public float maxHealth = 100.0f;
    public float moveSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public float moveNoiseAmount = 3f;
    public float runNoiseAmount = 6f;
    // public BaseWeapon startingWeaponPrefab = null;

    [HideInInspector]
    public float health;

    // Inputs
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 lookInput;
    [HideInInspector] public bool isFiring;
    [HideInInspector] public bool reload;
    [HideInInspector] public bool pickUpWeapon;
    private bool pickUpWeaponReleased = true;
    [HideInInspector] public bool dropWeapon;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isPushing;

    [HideInInspector] public float damageInflicted = 0f;
    [HideInInspector] public int killCount = 0;
    [HideInInspector] public int bulletsShot = 0;

    // private BaseWeapon m_CurrentWeapon;

    private CharacterController m_CharacterController = null;
    private bool m_LastIsFiring = false;
    SpriteRenderer playerSprite;


    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        // SetCurrentWeaponSlot(startingWeaponPrefab);
        health = maxHealth;
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 moveInput3D = new Vector3(moveInput.x, 0.0f, moveInput.y);

        if (GameManager.Instance.KeyboardMode)
        {
            isFiring = GameManager.Instance.KeyboardMode && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
            isRunning = GameManager.Instance.KeyboardMode && Input.GetKey(KeyCode.LeftShift);
            isPushing = Input.GetKeyUp(KeyCode.Space);

            int left = Input.GetKey(KeyCode.A) ? -1 : 0;
            int right = Input.GetKey(KeyCode.D) ? 1 : 0;

            int up = Input.GetKey(KeyCode.W) ? 1 : 0;
            int down = Input.GetKey(KeyCode.S) ? -1 : 0;

            moveInput3D = new Vector3(left + right, 0.0f, up + down);
        }
        m_CharacterController.SimpleMove(moveInput3D * (isRunning ? runSpeed : moveSpeed));

        // Rotate to look at gamepad target
        if (lookInput != Vector2.zero || GameManager.Instance.KeyboardMode)
        {
            Vector3 lookInput3D = new Vector3(lookInput.x, 0.0f, lookInput.y);

            if (GameManager.Instance.KeyboardMode)
            {
                int left = Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
                int right = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;

                int up = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
                int down = Input.GetKey(KeyCode.DownArrow) ? -1 : 0;

                lookInput3D = new Vector3(left + right, 0.0f, up + down);

                if (up + down != 0 || left + right != 0) {
                    transform.rotation = Quaternion.LookRotation(lookInput3D, Vector3.up);
                }
            } else
            {
                transform.rotation = Quaternion.LookRotation(lookInput3D, Vector3.up);
            }


        }

        if (isPushing)
        {
            Push();
        }
       

    }

    IEnumerator DamageFeedback() {
        playerSprite.material.color = Color.clear;
        yield return new WaitForSeconds(0.10f);
        playerSprite.material.color = Color.white;
        yield return null;
    }

    public void ReceiveDamage(float damage)
    {

        health -= damage;
        StartCoroutine(DamageFeedback());
        DamageFeedback();
        if (health <= 0.0f)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    // public void SetCurrentWeaponSlot(BaseWeapon weaponPrefab)
    // {
    //     // Destroy current weapon
    //     if(m_CurrentWeapon != null)
    //     {
    //         Destroy(m_CurrentWeapon.gameObject);
    //     }
    //     m_CurrentWeapon = null;

    //     // Spawn in new weapon
    //     if(weaponPrefab != null)
    //     {
    //         m_CurrentWeapon = Instantiate(weaponPrefab.gameObject, transform).GetComponent<BaseWeapon>();
    // 		m_CurrentWeapon.instigator = this;
    //     }
    // }


    public NoiseData GetNoiseData()
    {
        return new NoiseData()
        {
            amount = GetNoiseAmount(),
            source = transform.position
        };
    }

    public float GetNoiseAmount()
    {
        if (moveInput == Vector2.zero)
        {
            return 0;
        }

        return isRunning ? runNoiseAmount : moveNoiseAmount;
    }

    public void OnExplosionHit(float damage)
    {
        ReceiveDamage(damage);
    }

    void Push()
    {
        RaycastHit hit2;
        Vector3 position = transform.position + transform.forward.normalized * 0.25f;
        float radius = 0.5f;
        Vector3 direction = transform.forward;
        float attackRange = 0.5f;

        Debug.DrawRay(position, direction.normalized * (attackRange + radius), Color.red);

        float knockBackAmount = 0.3f;
        if (Physics.SphereCast(position, radius, direction, out hit2, attackRange, 1 << LayerMask.NameToLayer("Zombies")))
        {
            RaycastHit[] hits = Physics.SphereCastAll(position, radius, direction, attackRange, 1 << LayerMask.NameToLayer("Zombies"));
            foreach (RaycastHit hit in hits)
            {
                bool zombieHit = hit.collider.GetComponentInParent<ZombieHealth>() != null;
                if (!zombieHit) continue;

                GameObject go = hit.collider.GetComponentInParent<ZombieHealth>().gameObject;
                NavMeshAgent nav = go.GetComponent<NavMeshAgent>();

                Vector3 npcPos = go.transform.position;
                Vector3 direction1 = (npcPos - transform.position).normalized;
                direction1 = direction1 * knockBackAmount / 2;
                direction1 = new Vector3(direction1.x, 0f, direction1.z);

                nav.Warp(npcPos + direction1);
            }

            Debug.Log("Zombie Hit");
        }
    }

    public bool DropWeapon
    {
        get {
            return dropWeapon;
        }
    }

    public Weapon PickUpWeapon
    {
        get {
            if (!pickUpWeapon) {
                pickUpWeaponReleased = true;
                return null;
            } else if (!pickUpWeaponReleased) {
                return null;
            }
            pickUpWeaponReleased = false;
            //TODO: This is horribly inefficient. Fix it. --Toke, the embarrased author
            Weapon[] weapons = FindObjectsOfType<Weapon>();
            Weapon weapon = null;
            float smallestDistance = float.PositiveInfinity;
            for (int i = 0; i < weapons.Length; i++) {
                if (!weapons[i].Equipped) {
                    float distance = (weapons[i].transform.position - transform.position).magnitude;
                    if (distance <= GetComponent<WeaponInventory>().PickupRange && (weapon == null || distance < smallestDistance)) {
                        weapon = weapons[i];
                        smallestDistance = distance;
                    }
                }
            }
            return weapon;
        }
    }

    public bool Fire
    {
        get
        {
            return isFiring;
        }
    }
    public bool Reload
    {
        get
        {
            if (reload) return true;
            Weapon weapon = GetComponent<WeaponInventory>().Weapon;
            if (weapon == null) return false;
            ProjectileWeapon projectileWeapon = weapon.GetComponent<ProjectileWeapon>();
            return projectileWeapon != null && projectileWeapon.Ammunition == 0;
        }
    }
}
    