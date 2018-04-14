using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/States/Wander")]
public class WanderState : State
{
    public override void EnterState(StateController controller)
    {
        base.EnterState(controller);
        controller.GenerateNewWanderTarget();
    }
}
