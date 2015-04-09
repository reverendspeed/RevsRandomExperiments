using UnityEngine;
using System.Collections;

public class CharacterMotorThirdP : MonoBehaviour {

	private	Vector3			moveInput; // Store input so char doesn't reset orientation each update
	public	float			moveForce			= 4.0F;

	private	bool			isJumping;
	public 	float			jumpForce			= 5.0F;
	public	float			isGroundedDistance	= 0.32F;
	private	Vector3			trailingPlayerFeet; // Vec3 that trails player, provides jump forgiveness
	public	float			trailingPlayerFeetSpeed	= 4.0F;
	public	LayerMask		ignorePlayerMask;
	 
	private Rigidbody 		rb;

	public	Vector3			lastFramesVelocity;

	void Start () {
		rb 			= GetComponent<Rigidbody> ();
		trailingPlayerFeet 	= transform.position + (Vector3.up * 0.1F); // Start forgiveness trail at player pos
	}

	void Update () {
		moveInput	= new Vector3 (Input.GetAxis ("Horizontal"), 0.0F, Input.GetAxis ("Vertical"));

		Vector3 playerFeet = transform.position + (Vector3.up * 0.1f); // Get current base, a little inside collider

		if (Input.GetButtonDown ("Jump")) {
			if (IsGrounded(playerFeet)){ // If touching ground, then jump!
				isJumping	= true;
			} else {
				if (IsGrounded(trailingPlayerFeet)){ // ...Otherwise check if can touch ground from trailing vector3...
					isJumping	= true; // ... and if so, allow player to jump even though they're probably falling!
				}
			}
		}
		trailingPlayerFeet = Vector3.Lerp(trailingPlayerFeet, playerFeet, Time.deltaTime * trailingPlayerFeetSpeed);

		lastFramesVelocity = rb.velocity;
	}

	void FixedUpdate () {
		if (Mathf.Abs(moveInput.x) > float.Epsilon || Mathf.Abs (moveInput.z) > float.Epsilon) { // If there's movement input...
			rb.MoveRotation (Quaternion.LookRotation (moveInput, Vector3.up)); // ... apply rotation...
			rb.MovePosition (rb.position + moveInput * Time.deltaTime * moveForce); // ... apply movement.
		}

		if (isJumping){
			// rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse); // Using the physics function to change velocity
			rb.velocity += new Vector3(0, jumpForce, 0); // Changing velocity directly
		}
		isJumping = false;
		rb.angularVelocity = Vector3.zero; // Prevent rigidbody from spinning after touching another rigidbody
	}

	public	void ApplyPlatformVelocity (Vector3 platVelocity){ // This function allows platforms the player is standing on to alter player's velocity.
		rb.velocity = new Vector3(platVelocity.x, rb.velocity.y, platVelocity.z);
	}

	bool IsGrounded (Vector3 testPoint) { // Raycast downwards from feet to find if on a surface
		if (Physics.Raycast (testPoint, Vector3.down, isGroundedDistance, ignorePlayerMask)) {
			Debug.DrawRay (testPoint, Vector3.down * isGroundedDistance, Color.green);
			return true;
		} else {
			Debug.DrawRay (testPoint, Vector3.down * isGroundedDistance, Color.red);
			return false;
		}
	}
}