using UnityEngine;
using System.Collections;

public class CatMove : MonoBehaviour {

	// public 	Transform 	catRotate;
	public 	float 			speed 			= 6.0F;
	public 	float 			jumpSpeed 		= 8.0F;
	public 	float 			gravity 		= 20.0F;

	public	float			testGroundDist	= 0.64F;
	public	LayerMask		testGroundLayer;

	private	Rigidbody		rb;
	public Vector3			moveDirection;

	void	Awake () {
		rb 				= GetComponent<Rigidbody> ();
	}

	bool	IsGrounded () {
		Debug.DrawRay (transform.position, -Vector3.up * testGroundDist, Color.red);
		if(Physics.Raycast( transform.position, -Vector3.up, testGroundDist, testGroundLayer)){
			return true;
		}else{
			return false;
		}
	}

	public	void MoveCat (Vector3 inputMoveDirection, bool jump){
		Quaternion deltaRotation = Quaternion.LookRotation (moveDirection, Vector3.up); // Quaternion.Euler(moveDirection * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);

		if (IsGrounded ()) {
			moveDirection = inputMoveDirection;
			moveDirection = transform.TransformDirection(moveDirection);
			// if(moveDirection != Vector3.zero)Rotate (moveDirection);
			moveDirection *= speed; // Should I multiply this here, given I've yet to do jumping?
			if (jump)	moveDirection.y = jumpSpeed;
		}
		rb.MovePosition (rb.position + moveDirection * Time.deltaTime);


	}
	
	//	void Rotate(Vector3 catTarget){
//		Quaternion rotation = Quaternion.LookRotation(catTarget);
//		catRotate.rotation = rotation;
//	}
}
