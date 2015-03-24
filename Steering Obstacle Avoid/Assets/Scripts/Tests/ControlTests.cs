using UnityEngine;
using System.Collections;

public class ControlTests : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown("A_1"))	Debug.Log ("A_1 pressed.");
		if(Input.GetButtonDown("B_1"))	Debug.Log ("B_1 pressed.");
		if(Input.GetButtonDown("X_1"))	Debug.Log ("X_1 pressed.");
		if(Input.GetButtonDown("Y_1"))	Debug.Log ("Y_1 pressed.");

		if(!(Input.GetAxis ("L_XAxis_1") <= 0.4f 	&& Input.GetAxis ("L_XAxis_1") >= -0.4f)) Debug.Log (Input.GetAxis ("L_XAxis_1"));
		if(!(Input.GetAxis ("L_YAxis_1") <= 0.4f 	&& Input.GetAxis ("L_YAxis_1") >= -0.4f)) Debug.Log (Input.GetAxis ("L_YAxis_1"));

		if(Input.GetButtonDown("A_2"))	Debug.Log ("A_2 pressed.");
		if(Input.GetButtonDown("B_2"))	Debug.Log ("B_2 pressed.");
		if(Input.GetButtonDown("X_2"))	Debug.Log ("X_2 pressed.");
		if(Input.GetButtonDown("Y_2"))	Debug.Log ("Y_2 pressed.");
		
		if(!(Input.GetAxis ("L_XAxis_2") <= 0.4f 	&& Input.GetAxis ("L_XAxis_2") >= -0.4f)) Debug.Log (Input.GetAxis ("L_XAxis_2"));
		if(!(Input.GetAxis ("L_YAxis_2") <= 0.4f 	&& Input.GetAxis ("L_YAxis_2") >= -0.4f)) Debug.Log (Input.GetAxis ("L_YAxis_2"));
	}
}
