using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{

    public override bool Decide(StateController controller)
    {
		return Scan(controller);
    }

	private bool Scan(StateController controller)
	{
	    Collider[] players = Physics.OverlapSphere(controller.transform.position, controller.enemyStats.scanForPlayerRadius, 1 << LayerMask.NameToLayer("Players"));
	    if (players.Length > 0)
	    {
	        controller.chaseTarget = players[0].transform;
	        return true;
	    }
	    else
	    {
	        return false;
	    }
	}

}