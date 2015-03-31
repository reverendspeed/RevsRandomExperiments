using UnityEngine;
using System.Collections;

public class PlatformPingPong : MonoBehaviour {

	public	float	distance 	= 1.0F;
	public	float	speed 		= 1.0F;

	public	Transform	positionStart;
	public	Transform	positionEnd;

	private	Vector3		oldPos;
	private	Vector3		newPos;
	private	Vector3		calculatedVelocity;

	private	Rigidbody	rb;
	private	CharacterMotorThirdP player;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotorThirdP> ();
		oldPos = transform.position;
	}

	void Update() {
		PingPongPlatform ();

		CalculateVelocity ();
	}

	void PingPongPlatform () {
		float currentLerp = Mathf.PingPong(Time.time * speed, distance);
		transform.position = Vector3.Lerp (positionStart.position, positionEnd.position, currentLerp);
	}

	void CalculateVelocity() {
		newPos = transform.position;
		calculatedVelocity = (newPos - oldPos) / Time.deltaTime;
		oldPos = newPos;
	}

	void OnCollisionEnter (Collision other) { // Poss. should be OnTriggerEnter - w/a trig vol above plat surface
		if(other.gameObject.tag == "Player") { // Allows player to stand on platform w/out sliding
			other.transform.parent = transform; 
		}
		player.ApplyPlatformVelocity (rb.velocity); // Reset player velocity on touching platform
	}
	
	void OnCollisionExit (Collision other) { // Poss. should be OnTriggerExit
		if(other.gameObject.tag == "Player") { // Unparent player upon dismount
			other.transform.parent = null; 
		}
		player.ApplyPlatformVelocity (calculatedVelocity); // Player inherits platform calculated velocity on dismount
	}
}