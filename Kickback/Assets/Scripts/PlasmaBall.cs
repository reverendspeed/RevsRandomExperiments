using UnityEngine;
using System.Collections;

public class PlasmaBall : MonoBehaviour {

	private Rigidbody 	rb;
	private	Vector3		originalScale;

	public	float			flashSize	= 2.0F;
	public	float			flashTime	= 0.25F;
	public	float			lifeTime 	= 4.0F;
	public	float			speed 		= 4.0F;
	[HideInInspector]
	public	MeshRenderer	meshRenderer;
	[HideInInspector]
	public	Collider		collidr;

	void Awake () {
		rb 				= GetComponent<Rigidbody> ();
		meshRenderer 	= GetComponent<MeshRenderer> ();
		collidr 		= GetComponent<Collider> ();
		originalScale 	= transform.localScale;
	}

	void OnEnable () {
		Invoke ("DisableMe", lifeTime);
		StartCoroutine (MuzzleFlash ());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		rb.MovePosition (rb.position + transform.forward * GameManager.instance.deltaTime * speed);
	}

	void OnCollisionEnter(Collision other){

		CancelInvoke ();
		DisableMe ();
	}

	void DisableMe () {
		gameObject.SetActive (false);
	}

	void OnDisable () {
		CancelInvoke ();
	}

	IEnumerator MuzzleFlash () {
		transform.localScale = originalScale * flashSize;
		yield return new WaitForSeconds (flashTime);
		transform.localScale = originalScale;
	}

//	void SetMaterial (
}
