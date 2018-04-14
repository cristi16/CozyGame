using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Wander")]
public class WanderAction : Action
{

    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    void Wander(StateController controller)
    {
        controller.navMeshAgent.destination = controller.nextWanderTarget;
        controller.navMeshAgent.isStopped = false;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.GenerateNewWanderTarget();
        }
    }

}