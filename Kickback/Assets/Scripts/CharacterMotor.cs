using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpeedValues {
	public	float		groundSpeed 		= 4.0F;
	public	float		airScalar			= 0.75F;
	public	float		runScalar			= 2.0F;
	public	float		jumpForce			= 5.12F;
	public	float		jumpMercyTestSpeed 	= 4.0F;
	public	float		movSlerpSpeed 		= 3.84F;
	public	float		rotSlerpSpeed 		= 7.68F;
	[HideInInspector]
	public	float		moveSpeed;
}

[System.Serializable] 
public class VeloClamp{
	public	float		maxYVelocity 	= 05.12F;
	public	float		minYVelocity 	= 10.24F;
	public	float		maxXZVelocity 	= 10.24F;
	[HideInInspector]
	public	Vector3		nowVClamp;
}

[System.Serializable]
public	class InputValues {
	[HideInInspector]
	public	Vector3		moveInput;
	[HideInInspector]
	public	Vector3		rotInput;
	[HideInInspector]
	public	bool		jumpInput 		= false;
	public 	enum AimingType	{
		secondStick,
		mouse
	};
	
	public	AimingType	aimingType;
	[HideInInspector]
	public	bool		mouseActive		= false;
	[HideInInspector]
	public	float		mouseTimer		= 0.0F;
	public	float		mouseTimeOut	= 2.0F;
	public	bool		mayWalljump		= true;
	public	bool		mayDoublejump	= true;
	[HideInInspector]
	public	bool		canDoublejump 	= true;
}

public class CharacterMotor : MonoBehaviour {

	public SpeedValues 	speeds;
	public VeloClamp 	vClamp;
	public InputValues 	inputValues;

	public	LayerMask	playerMask;

	private	Vector3		groundTestPos;
	private	Vector3		jumpMercyTestPos;
	private	float		groundRayDist	= 1.28F;
	private	bool		grounded		= true;

	private	float		wallJumpRayDist	= 1.28F;

	public	Transform	worldCursorObject;
	public	Camera		worldCursorCamera;

	private	Transform	originalParent;
	private	Rigidbody	rb;
	private	Vector3		rbVelocity;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		speeds.moveSpeed = speeds.groundSpeed;

		GetGroundTestPos ();
		jumpMercyTestPos = groundTestPos;

		originalParent= transform.parent;

		StartCoroutine (MonitorMouseInput ());
	}

	void Start () {
	
	}

	void Update () {

		HandleMoveRotInput ();

		GetGroundTestPos ();

		jumpMercyTestPos = Vector3.Lerp (jumpMercyTestPos, groundTestPos, speeds.jumpMercyTestSpeed * Time.deltaTime);

		HandleRunningInput ();

		HandleJumpingInput ();
	}

	void FixedUpdate () {

		ClampVelocity ();

		PerformMovement ();

		PerformTwinstickRotation ();

		PerformJumping ();

		rb.angularVelocity = Vector3.zero; // Prevent rigidbody from spinning after touching another rigidbody
	}

	void OnCollisionEnter () {
		if (IsGrounded (groundTestPos)) {
			inputValues.canDoublejump = false;
		}
	}

	void GetGroundTestPos () {
		groundTestPos = transform.position + Vector3.up;
	}

	void HandleMoveRotInput () {
		Vector3 targetMoveInput = new Vector3 (Input.GetAxis ("Horizontal"), 0.0F, Input.GetAxis ("Vertical"));
		inputValues.moveInput	= Vector3.Slerp(inputValues.moveInput, targetMoveInput, speeds.rotSlerpSpeed * Time.deltaTime);

		switch (inputValues.aimingType) {
		case InputValues.AimingType.mouse:	{
			Vector3 mousePosition = worldCursorCamera.ScreenToViewportPoint(Input.mousePosition); 
			Ray	worldCursorRay = worldCursorCamera.ViewportPointToRay(mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(worldCursorRay, out hit)){
				worldCursorObject.position 	= hit.point;
				if(inputValues.mouseActive){
					Vector3 dirFromPoint	= hit.point - transform.position;
					Vector3 targetRotInput	= new Vector3 (dirFromPoint.x, 0.0F, dirFromPoint.z);
					inputValues.rotInput	= targetRotInput;
				}else {
					inputValues.rotInput	= Vector3.zero;
				}
			}
		}
			return;
			case InputValues.AimingType.secondStick:	{
				Vector3 targetRotInput	= new Vector3 (Input.GetAxis ("Horizontal2"), 0.0F, Input.GetAxis ("Vertical2"));
				inputValues.rotInput			= Vector3.Slerp(inputValues.rotInput, targetRotInput, speeds.rotSlerpSpeed * Time.deltaTime); 
			}
			return;
		}
	}

	IEnumerator MonitorMouseInput(){
		while (enabled) {
			if(Input.GetAxis ("Mouse X")>0.0F || Input.GetAxis ("Mouse Y")>0.0F){
				inputValues.mouseActive = true;
				inputValues.mouseTimer  = 0.0F;
			}else{
				inputValues.mouseTimer += Time.deltaTime;
				if(inputValues.mouseTimer >= inputValues.mouseTimeOut){
					inputValues.mouseActive = false;
				}
			}
			yield return null;
		}
	}

	void HandleRunningInput () {
		if (Input.GetButton ("Fire2") && grounded) {
			speeds.moveSpeed = speeds.groundSpeed * speeds.runScalar;
		} 
		
		if (Input.GetButtonUp ("Fire2") && grounded){
			speeds.moveSpeed = speeds.groundSpeed;
		}
	}

	void ClampVelocity () {
		vClamp.nowVClamp = rb.velocity;
		vClamp.nowVClamp = new Vector3 (Mathf.Clamp(vClamp.nowVClamp.x, -vClamp.maxXZVelocity, vClamp.maxXZVelocity),
		                                Mathf.Clamp(vClamp.nowVClamp.y, -vClamp.minYVelocity, vClamp.maxYVelocity),
		                                Mathf.Clamp(vClamp.nowVClamp.z, -vClamp.maxXZVelocity, vClamp.maxXZVelocity));
		rb.velocity = vClamp.nowVClamp;
	}

	void PerformMovement () {
		if (Mathf.Abs(inputValues.moveInput.x) > 0.01F || Mathf.Abs(inputValues.moveInput.z) > 0.01F) {
			Vector3 finalMoveInput = rb.position + inputValues.moveInput * GameManager.instance.deltaTime * speeds.moveSpeed;
			rb.MovePosition (finalMoveInput);
			// Poss use addForce here to provide momentum? forum.unity3d.com/threads/rigidbodies-fast-movement-with-small-inertia.306321/
			rb.MoveRotation (Quaternion.LookRotation(inputValues.moveInput, Vector3.up));
		}
	}

	void PerformTwinstickRotation () {
		if (Mathf.Abs(inputValues.rotInput.x) > 0.01F || Mathf.Abs(inputValues.rotInput.z) > 0.01F) {
			rb.MoveRotation (Quaternion.LookRotation(inputValues.rotInput, Vector3.up));
		}
	}

	void HandleJumpingInput (){
		if (Input.GetButtonDown ("Jump")) {
			if (IsGrounded (groundTestPos) && !inputValues.canDoublejump) {
				speeds.moveSpeed = speeds.moveSpeed * speeds.airScalar;
				inputValues.jumpInput = true;
				inputValues.canDoublejump = true;
				grounded = false;
			} else if (IsGrounded (jumpMercyTestPos) && !inputValues.canDoublejump){
				speeds.moveSpeed = speeds.moveSpeed * speeds.airScalar;
				inputValues.jumpInput = true;
				inputValues.canDoublejump = true;
				grounded = false;
			} else if (CanWallJump () && inputValues.mayWalljump) {
				inputValues.jumpInput = true;
				inputValues.canDoublejump = true;
				grounded = false;
			} else if (inputValues.canDoublejump && inputValues.mayDoublejump) {
				inputValues.canDoublejump = false;
				inputValues.jumpInput = true;
			} else {
				inputValues.jumpInput = false;
			}
		}
	}

	void PerformJumping () {
		if (inputValues.jumpInput) {
			rb.velocity = new Vector3 (rb.velocity.x, 0.0F, rb.velocity.z);
			rb.AddForce(rb.velocity + (speeds.jumpForce * Vector3.up), ForceMode.Impulse);
			inputValues.jumpInput = false;
		}
	}

	bool IsGrounded (Vector3 testPoint) {
		if(Physics.Raycast(testPoint, Vector3.down, groundRayDist,playerMask)){ 
			Debug.DrawRay(testPoint, Vector3.down * groundRayDist, Color.green);
			speeds.moveSpeed = speeds.groundSpeed;
			grounded = true;
			return true;
		}else{
			Debug.DrawRay(testPoint, Vector3.down * groundRayDist, Color.red);
			speeds.moveSpeed = speeds.groundSpeed * speeds.airScalar;
			grounded = false;
			return false;
		}
	}

	bool CanWallJump () {
		if (Physics.Raycast (transform.position, inputValues.moveInput.normalized, wallJumpRayDist, playerMask)) {
			Debug.DrawRay(transform.position, inputValues.moveInput.normalized * wallJumpRayDist, Color.green);
			return true;
		}else{
			Debug.DrawRay(transform.position, inputValues.moveInput.normalized * wallJumpRayDist, Color.red);
			return false;
		}
	}

	public void KickBack (float	force){
		rb.MovePosition (rb.position + transform.forward * -force * GameManager.instance.deltaTime);  	
	}

	public	void ApplyPlatformVelocity (Vector3 platVelocity){ // This function allows platforms the player is standing on to alter player's velocity.
		rb.velocity = new Vector3(platVelocity.x, rb.velocity.y, platVelocity.z);
	}

	public	void DismountPlatformReset () {
		transform.SetParent(originalParent);
	}
}
