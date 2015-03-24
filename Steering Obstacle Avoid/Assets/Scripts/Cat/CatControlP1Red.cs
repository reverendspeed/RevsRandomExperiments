using UnityEngine;
using System.Collections;

public class CatControlP1Red : MonoBehaviour {
	private	CatMove		catMove;
	private Vector3 	moveDirection = Vector3.zero;
	private	bool		jump	= false;
	
	void Awake () {
		catMove 		= GetComponent<CatMove> ();
	}
	
	void Update() {
		jump 			= false;
		moveDirection 	= new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		jump			= Input.GetButtonDown("Jump");

	}

	void FixedUpdate () {
		if (jump || Mathf.Abs(moveDirection.x) > 0.1F || Mathf.Abs(moveDirection.z) > 0.1F) {
			catMove.MoveCat (moveDirection, jump);
		}
	}
}
