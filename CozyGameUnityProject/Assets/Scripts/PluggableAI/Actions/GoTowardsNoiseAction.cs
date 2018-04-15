using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/GoTowardsNoise")]
public class GoTowardsNoiseAction : Action
{

    public override void Act(StateController controller)
    {
        GoTowardsNoise(controller);
    }

    private void GoTowardsNoise(StateController controller)
    {
        controller.navMeshAgent.destination = controller.followNoiseTarget;
        controller.navMeshAgent.isStopped = false;
    }

}