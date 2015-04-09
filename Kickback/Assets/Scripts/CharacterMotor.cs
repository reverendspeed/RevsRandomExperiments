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
		if (Input.GetButtonDown ("Jump")) {
			if (IsGrounded()){
				jumpInput = true;
			}else{
				jumpInput = false;
			}
		}
	}

	void FixedUpdate () {
		if (Mathf.Abs(moveInput.x) > float.Epsilon || Mathf.Abs(moveInput.z) > float.Epsilon) {
//			rb.MovePosition (rb.position + moveInput * Time.deltaTime * moveSpeed); // Get rid of time.deltatime
			rb.MovePosition (rb.position + moveInput * Time.deltaTime * moveSpeed); // Get rid of time.deltatime
			rb.MoveRotation (Quaternion.LookRotation(moveInput, Vector3.up));
		}

		if (jumpInput) {
			rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
			jumpInput = false;
		}
	}

	bool IsGrounded () {
		if(Physics.Raycast(transform.position, Vector3.down, groundRayLen,playerMask)){ 
			Debug.DrawRay(transform.position, Vector3.down * groundRayLen, Color.green);
			return true;
		}else{
			Debug.DrawRay(transform.position, Vector3.down * groundRayLen, Color.red);
			return false;
		}
	}

	public	void	KickBack (float	force){
		rb.MovePosition (rb.position + transform.forward * -force * Time.deltaTime);  	
	}
}
