using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager s_Instance = null;
    public static GameManager Instance { get { return s_Instance; } }

    public PlayerCharacterController[] players;

    private void Start()
    {
        s_Instance = this;

        // Spawn players that were activated in the menu
        for(int i = 0; i < 4; ++i)
        {
            if(CozyInputManager.Instance.IsControllerPlaying(i))
            {
                OnPlayerJoined(i);
            }
        }

    }
    private void OnDestroy()
    {
        s_Instance = null;
    }

    public void OnPlayerJoined(int controllerIndex)
    {
        players[controllerIndex].gameObject.SetActive(true);
    }

    public List<PlayerCharacterController> GetActivePlayers()
    {
        var result = new List<PlayerCharacterController>();
        for(int i = 0; i < 4; ++i)
        {
            if (players[i].gameObject.activeSelf)
                result.Add(players[i]);
        }
        return result;
    }

}
