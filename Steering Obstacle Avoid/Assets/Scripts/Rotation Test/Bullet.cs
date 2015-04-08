using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public	float		speed 		= 10.0F;
	public	float		lifeTime 	= 2.0F;
	
	private	Rigidbody	rb;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void OnEnable () {
		Invoke ("Destroy", lifeTime);
	}

	void FixedUpdate () {
		rb.MovePosition (rb.position + transform.forward * speed * Time.deltaTime);
	}

	void Deactivate () {
		gameObject.SetActive (false);
	}

	void OnDisable () {
		CancelInvoke ();
	}

	void OnCollisionEnter () {
		Debug.Log ("Bullet hit something!");
		Deactivate ();
	}
}
