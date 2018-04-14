using UnityEngine;
using XInputDotNetPure;
using System.Collections.Generic;

public class CozyInputManager : MonoBehaviour {
    public PlayerCharacterController[] players;
	public int activePlayers=0;

    void Update()
    {
        for(int i = 0; i < 4; ++i)
        {
            GamePadState state = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.Circular);
            
            if(!players[i].gameObject.activeSelf)
            {
                // Player log in by pressing A
                if(state.Buttons.A == ButtonState.Pressed)
                {
                    players[i].gameObject.SetActive(true);
					activePlayers++;
                }
            }
            else
            {
                // Handle logged in player input
                players[i].moveInput = new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
                players[i].lookInput = new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
                players[i].isFiring = state.Triggers.Right > 0.5f;
            }
        }
    }
}
