using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

	[SerializeField]
	Text[] playerScoreText;

	void Update ()
    {
		for (int i = 0; i < 4; i++)
        {
            if (CozyInputManager.Instance.IsControllerPlaying(i))
            {
                playerScoreText [i].gameObject.SetActive (true);
                playerScoreText [i].text = "P" + (i+1) + ": " + GameManager.Instance.players[i].killCount + " kills";
            }
            else
            {
                playerScoreText [i].text = "";
                playerScoreText [i].gameObject.SetActive (false);
            }
		}
	}

}
