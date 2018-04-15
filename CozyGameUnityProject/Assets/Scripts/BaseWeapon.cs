using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
	public PlayerCharacterController instigator;
    public virtual void StartFiring() { }
    public virtual void StopFiring() { }
}
