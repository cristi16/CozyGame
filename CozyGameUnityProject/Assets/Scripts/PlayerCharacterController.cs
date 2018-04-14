using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    [HideInInspector] public Vector2 moveInput;
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