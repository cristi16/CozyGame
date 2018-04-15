using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

	[SerializeField]
	Text[] playerScoreText;

	[SerializeField]
	CozyInputManager gameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < gameManager.activePlayers.Count; i++) {
			playerScoreText [i].gameObject.SetActive (true);
			playerScoreText [i].text = "P" + (i+1) + ": " + gameManager.activePlayers [i].killCount + " kills";
		}

		for (int j = gameManager.activePlayers.Count; j < playerScoreText.Length; j++) {
			playerScoreText [j].text = "";
			playerScoreText [j].gameObject.SetActive (false);
		}			
	}

}
