using UnityEngine;
using System.Collections;

public class SimpleAvoidance : MonoBehaviour {

	public	Transform target;

	private	Rigidbody	rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = (target.position - transform.position).normalized;
		RaycastHit hit;
		// Check for forward raycast
		if (Physics.Raycast (transform.position, transform.forward, out hit, 8)) {
			if(hit.transform != transform){
				Debug.DrawLine(transform.position, hit.point, Color.red);
				dir += hit.normal * 2;
			}
		}

		Vector3 leftR 	= transform.position;
		Vector3 rightR 	= transform.position;

		leftR.x 	-= 2;
		rightR.x 	+= 2;

		if (Physics.Raycast (leftR, transform.forward, out hit, 4)) {
			if(hit.transform != transform){
				Debug.DrawLine(leftR, hit.point, Color.red);
				dir += hit.normal * 2;
			}
		}

		if (Physics.Raycast (rightR, transform.forward, out hit, 4)) {
			if(hit.transform != transform){
				Debug.DrawLine(rightR, hit.point, Color.red);
				dir += hit.normal * 2;
			}
		}

		Quaternion rot = Quaternion.LookRotation (dir);

		// transform.rotation = Quaternion.Slerp (transform.rotation, rot, 5 * Time.deltaTime);
		rb.MoveRotation(Quaternion.Slerp(rb.rotation, rot, 5 * Time.deltaTime));
		// transform.position += transform.forward * 5 * Time.deltaTime;
		rb.MovePosition(rb.position + transform.forward * 2 * Time.deltaTime);
	}
}
