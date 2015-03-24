using UnityEngine;
using System.Collections;

public class MoveRotateTest : MonoBehaviour {

	public 	float	jumpForce	= 10.0F;

	private Rigidbody rb;

	public	Vector3	stickInput;
	public	bool	jump;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		stickInput 	= new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
		if (Input.GetButtonDown ("Jump")) {
			jump = true;
		}
	}

	void FixedUpdate () {
		if (Mathf.Abs(stickInput.x) > 0.1F || Mathf.Abs (stickInput.z) > 0.1F) {
			rb.MoveRotation (Quaternion.LookRotation (stickInput, Vector3.up));
			rb.MovePosition (rb.position + stickInput * Time.deltaTime);
		}
		if (jump){
			rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			jump 		= false;
		}
	}
}
