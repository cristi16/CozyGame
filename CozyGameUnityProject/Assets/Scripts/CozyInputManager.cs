using UnityEngine;
using XInputDotNetPure;
using System.Collections.Generic;

public class CozyInputManager : MonoBehaviour {
    private static CozyInputManager s_Instance = null;
    public static CozyInputManager Instance { get { return s_Instance; } }
    public bool allowJoinInProgress = true;

    private LobbyUserInterface m_LobbyManager;
    private bool[] m_PlayerJoined = new bool[4];

    public void SetLobbyManager(LobbyUserInterface lobby)
    {
        m_LobbyManager = lobby;
    }

    public bool IsControllerPlaying(int index)
    {
        return m_PlayerJoined[index] || (index == 0 && GameManager.Instance.KeyboardMode);        
    }

    void Awake()
    {
        if(s_Instance != null)
        {
            Destroy(this);
        }
        else
        {
            s_Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void OnDestroy()
    {
        s_Instance = null;
    }

    void Update()
    {
		for(int i = 0; i < 4; ++i)
        {
            GamePadState state = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.Circular);            

            // Player joining
            if (!m_PlayerJoined[i])
            {
                if ((m_LobbyManager != null || allowJoinInProgress) && state.Buttons.A == ButtonState.Pressed)
                {
                    m_PlayerJoined[i] = true;
                    if (m_LobbyManager != null) m_LobbyManager.OnPlayerJoined(i);
                    if (GameManager.Instance != null) GameManager.Instance.OnPlayerJoined(i);
                }
                continue;
            }

            if(m_LobbyManager != null)
            {
                // When in lobby you can just press start
                if(state.Buttons.Start == ButtonState.Pressed)
                {
                    m_LobbyManager.OnStartPressed(i);
                }
            }
            else if(GameManager.Instance != null)
            {
                // When in game redirect input to player character
                GameManager.Instance.players[i].moveInput = new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
                GameManager.Instance.players[i].lookInput = new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
                GameManager.Instance.players[i].isFiring = state.Triggers.Right > 0.5f;
                GameManager.Instance.players[i].reload = state.Buttons.X == ButtonState.Pressed;				
				GameManager.Instance.players[i].isPushing = state.Buttons.LeftShoulder == ButtonState.Released;
            }
        }
    }
}
