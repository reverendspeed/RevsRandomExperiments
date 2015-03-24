using UnityEngine;
using System.Collections;

public class horribleTitleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel("catMoveTest01003Rev");
		}
	}
}
