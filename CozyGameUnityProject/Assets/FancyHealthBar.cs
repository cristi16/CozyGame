using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyHealthBar : MonoBehaviour {

	public SimpleHealthBar healthBar;
	public SimpleHealthBar killCount;

	void Start(){		
		UpdateKillCount (0);
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateHealthBar ( float currentValue, float maxValue )
	{
		healthBar.UpdateBar (currentValue, maxValue);
	}

	public void UpdateKillCount ( float currentValue, float maxValue=20 )
	{
		killCount.UpdateBar (currentValue%maxValue, maxValue);
	}

}
