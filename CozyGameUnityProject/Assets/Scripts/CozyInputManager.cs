using UnityEngine;
using XInputDotNetPure;

public class CozyInputManager : MonoBehaviour {
    public PlayerCharacterController player0;

    void Update()
    {
        GamePadState state = GamePad.GetState(0, GamePadDeadZone.Circular);
        player0.moveInput = new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
        player0.lookInput = new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);

        player0.isFiring = state.Triggers.Right > 0.5f;
    }
}
