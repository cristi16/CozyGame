using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUserInterface : MonoBehaviour {

    public UI.Text[] controllerTextObjects = new UI.Text[4];

    void Start()
    {
        CozyInputManager.Instance.SetLobbyManager(this);
    }

    void OnDestroy()
    {
        CozyInputManager.Instance.SetLobbyManager(null);
    }

    public void OnStartPressed(int playerIndex)
    {
        CozyInputManager.Instance.allowJoinInProgress = false;
        SceneManager.LoadScene(1);
    }

    public void OnPlayerJoined(int playerIndex)
    {
        controllerTextObjects[playerIndex].canvasRenderer.SetAlpha(0.0f);
        controllerTextObjects[playerIndex].gameObject.SetActive(true);
        controllerTextObjects[playerIndex].CrossFadeAlpha(1.0f, 0.5f, false);
    }

}
