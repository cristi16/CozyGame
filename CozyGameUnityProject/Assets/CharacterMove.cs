using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    public float speed = 10f;

    public bool playerOne = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerOne)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.I))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.K))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.J))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.L))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }    
    }
}
