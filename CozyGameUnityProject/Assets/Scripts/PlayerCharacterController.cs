using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public BaseWeapon startingWeaponPrefab = null;

    // Inputs
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 lookInput;
    [HideInInspector] public bool isFiring;


    private BaseWeapon m_CurrentWeapon;
    private CharacterController m_CharacterController = null;
    private bool m_LastIsFiring = false;

    void Start()
    {
        m_CurrentWeapon = Instantiate(startingWeaponPrefab.gameObject, transform).GetComponent<BaseWeapon>();
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        m_CharacterController.SimpleMove(new Vector3(moveInput.x, 0.0f, moveInput.y) * moveSpeed);

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
}