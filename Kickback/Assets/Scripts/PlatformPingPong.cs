using UnityEngine;
using System.Collections;

public class PlatformPingPong : MonoBehaviour {

	public	Transform 	positionOne;
	public	Transform	positionTwo;
	private	Vector3		targetPos;
	// public	Vector3		movePos; For Rigidbody movement, see FixedUpdate 

	public	bool		mayMove 		= true;
	public	float		targetSpeed 	= 1.0F;
	public	float		platformSpeed 	= 1.0F;
	private	float		distance 		= 1.0F;

	private	Vector3		oldPos;
	private	Vector3		calculatedVelocity;

	private	CharacterMotor player;
	private	Rigidbody rb;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotor> ();
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PerformPingPongMovement ();

		CalculateVelocity ();
	}

	void FixedUpdate () {
//		rb.MovePosition (movePos); REALLY WEIRD BEHAVIOR HERE
		// MovePosition with Kinematic causes Player to have huge sideways velocity on Play, for some reason.
		// MovePosition w/out Kinematic causes Player to jump once on Play. What in fuck?
		// Solution? Skip this shit, use transform.translate.
	}

	void PerformPingPongMovement () {
		float currentLerp = Mathf.PingPong(Time.time * targetSpeed, distance);
		targetPos = Vector3.Lerp (positionOne.position, positionTwo.position, currentLerp);
		transform.position = Vector3.Lerp (transform.position, targetPos, GameManager.instance.deltaTime * platformSpeed);
		//		movePos = Vector3.Lerp (positionOne.position, positionTwo.position, currentLerp); For Rigidbody movement, see FixedUpdate
	}

	void CalculateVelocity() {
		Vector3 newPos = transform.position;
		calculatedVelocity = (newPos - oldPos) / Time.deltaTime;
		oldPos = newPos;
	}

	void OnCollisionEnter (Collision other){
		if (other.transform.CompareTag ("Player")) {
			other.transform.SetParent(transform);
			// player.MountPlatformReset ();
		}
		player.ApplyPlatformVelocity (rb.velocity);
	}

	void OnCollisionExit (Collision other){
		if (other.transform.CompareTag ("Player")) {
			player.DismountPlatformReset();
		}
		player.ApplyPlatformVelocity (calculatedVelocity);
	}
}
