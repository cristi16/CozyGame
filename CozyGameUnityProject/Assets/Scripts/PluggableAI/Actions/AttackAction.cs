using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{

    public override void Act(StateController controller)
    {
		Attack(controller);
    }

	void Attack(StateController controller)
	{
		RaycastHit hit;
		Vector3 position = controller.eyes.position;
		float radius = controller.enemyStats.attackSphereCastRadius;
		Vector3 direction = controller.eyes.forward;
		float attackRange = controller.enemyStats.attackRange;

		Debug.DrawRay(position, direction.normalized * (attackRange + radius), Color.red);

        if (Physics.SphereCast(position, radius, direction, out hit, attackRange) && hit.collider.CompareTag("Player"))
		{
			if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
			{
			    PlayerCharacterController player = hit.collider.GetComponent<PlayerCharacterController>();
			    if (player != null)
			    {
                    Debug.Log("Deal damage to " + player.name);
			        player.ReceiveDamage(controller.enemyStats.attackDamage);
			        controller.stateTimeElapsed = 0f;
			    }
			}
		}
	}

}