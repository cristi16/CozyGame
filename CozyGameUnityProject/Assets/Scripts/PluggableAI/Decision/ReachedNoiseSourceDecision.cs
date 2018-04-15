using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachedNoiseSource")]
public class ReachedNoiseSourceDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return ReachedNoiseSource(controller);
    }

    bool ReachedNoiseSource(StateController controller)
    {
        return controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
               !controller.navMeshAgent.pathPending;
    }

}