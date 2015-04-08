using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour {

	public	float		moveSpeed  = 1.0F;
	private	Vector3		moveInput;

	public	float		jumpForce	= 8.0F;
	private	bool		jumpInput 	= false;

	public	LayerMask	playerMask;
	public	float		groundRayLen= 0.4F;

	private	Rigidbody	rb;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveInput = new Vector3 (Input.GetAxis ("Horizontal"), 0.0F, Input.GetAxis ("Vertical"));
		jumpInput = Input.GetButtonDown ("Jump");
	}

	void FixedUpdate () {
		if (Mathf.Abs(moveInput.x) > float.Epsilon || Mathf.Abs(moveInput.z) > float.Epsilon) {
//			rb.MovePosition (rb.position + moveInput * Time.deltaTime * moveSpeed); // Get rid of time.deltatime
			rb.MovePosition (rb.position + moveInput * moveSpeed); // Get rid of time.deltatime
			rb.MoveRotation (Quaternion.LookRotation(moveInput, Vector3.up));
		}

		if (jumpInput) {
			if(Physics.Raycast(transform.position - Vector3.up, -Vector3.up, groundRayLen,playerMask)){ 
				Debug.DrawRay(transform.position - Vector3.up, -Vector3.up, Color.green);
				rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
				jumpInput = false;
			}else{
				Debug.DrawRay(transform.position - (Vector3.up * 0.5F), -Vector3.up, Color.red);
			}
		}
	}

}
