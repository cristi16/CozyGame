using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    // Inputs
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 lookInput;

    [HideInInspector] public bool isFiring;
    public PistolWeapon currentWeapon;

    private CharacterController m_CharacterController = null;
    private bool m_LastIsFiring = false;

    void Start()
    {
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
        if(currentWeapon != null)
        {
            if(m_LastIsFiring != isFiring)
            {
                if(isFiring)
                {
                    currentWeapon.Fire();
                }
                m_LastIsFiring = isFiring;
            }
        }
    }
}