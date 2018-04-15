using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour, IGenerateNoise
{
	public float maxHealth = 100.0f;
	public float moveSpeed = 2.0f;
	public float runSpeed = 4.0f;
	public float moveNoiseAmount = 3f;
	public float runNoiseAmount = 6f;
	public BaseWeapon startingWeaponPrefab = null;

	[HideInInspector]
	public float health;    

    // Inputs
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 lookInput;
    [HideInInspector] public bool isFiring;
    [HideInInspector] public bool isRunning;

	[HideInInspector] public int damageInflicted=0;
	[HideInInspector] public int killCount=0;
	[HideInInspector] public int bulletsShot=0;

    private BaseWeapon m_CurrentWeapon;

    private CharacterController m_CharacterController = null;
    private bool m_LastIsFiring = false;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        SetCurrentWeaponSlot(startingWeaponPrefab);
		health = maxHealth;
    }

    void Update()
    {
        Vector3 moveInput3D = new Vector3(moveInput.x, 0.0f, moveInput.y);
        m_CharacterController.SimpleMove(moveInput3D * (isRunning ? runSpeed : moveSpeed));

        // Rotate to look at gamepad target
        if(lookInput != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(lookInput.x, 0.0f, lookInput.y), Vector3.up);
        }

        // Update weapon handling
        if(m_CurrentWeapon != null)
        {
            if(m_LastIsFiring != isFiring)
            {
                if(isFiring)
                {
                    m_CurrentWeapon.StartFiring();
                }
                else
                {
                    m_CurrentWeapon.StopFiring();
                }
                m_LastIsFiring = isFiring;
            }
        }
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
		gameObject.SetActive (false);
        //Destroy(gameObject);
    }

    public void SetCurrentWeaponSlot(BaseWeapon weaponPrefab)
    {
        // Destroy current weapon
        if(m_CurrentWeapon != null)
        {
            Destroy(m_CurrentWeapon.gameObject);
        }
        m_CurrentWeapon = null;

        // Spawn in new weapon
        if(weaponPrefab != null)
        {
            m_CurrentWeapon = Instantiate(weaponPrefab.gameObject, transform).GetComponent<BaseWeapon>();
			m_CurrentWeapon.instigator = this;
        }
    }


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
}