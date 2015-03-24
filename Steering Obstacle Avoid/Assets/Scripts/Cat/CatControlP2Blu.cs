using UnityEngine;
using System.Collections;

public class CatControlP2Blu : MonoBehaviour {
	private	CatMove		catMove;
	private Vector3 	moveDirection = Vector3.zero;

	void Awake () {
		catMove 		= GetComponent<CatMove> ();
	}

	void Update() {
		moveDirection 	= new Vector3(Input.GetAxis("L_XAxis_2"), 0, Input.GetAxis("L_YAxis_2"));
		bool	jump	= Input.GetButtonDown("A_2");
		catMove.MoveCat(moveDirection, jump);
	}
}
