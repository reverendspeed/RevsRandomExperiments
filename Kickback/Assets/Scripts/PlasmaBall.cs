using UnityEngine;
using System.Collections;

public class PlasmaBall : MonoBehaviour {

	private Rigidbody rb;

	public	float			lifeTime 	= 4.0F;
	public	float			speed 		= 4.0F;

	public	MeshRenderer	meshRenderer;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		meshRenderer = GetComponent<MeshRenderer> ();
	}

	void OnEnable () {
		Invoke ("DisableMe", lifeTime);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		rb.MovePosition (rb.position + transform.forward * speed);
	}

	void OnCollisionEnter(){
		CancelInvoke ();
		DisableMe ();
	}

	void DisableMe () {
		gameObject.SetActive (false);
	}

	void OnDisable () {
		CancelInvoke ();
	}

//	void SetMaterial (
}
