using UnityEngine;
using System.Collections;

public class JumpTest : MonoBehaviour {

	public	float		jumpForce 	= 01F;
	private	Rigidbody	rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		rb.AddForce (Vector3.up * jumpForce); 
	}
}
