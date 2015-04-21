using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {

	public	Material[] 	materials;
	public	float[]		bulletForce = new float[2] {0.0F, 1.0F};
	public	float		flashPeriod = 0.1F;
	public	float		hitTimeScale = 1.0F;

	private	Rigidbody	rb;

	private	MeshRenderer	meshRend;

	void Awake () {
		meshRend = GetComponent<MeshRenderer> ();
		materials[1] = meshRend.material;
		rb	= GetComponent<Rigidbody>();

//		bulletForce [0] = 0.0F;
//		bulletForce [1] = 1.0F;
	}

	void OnCollisionEnter (Collision other) {
		if (other.transform.CompareTag ("Bullet01")) {
			StartCoroutine (HitFlash());
			Vector3 moveDirection = rb.position - other.transform.position;
			rb.MovePosition (rb.position + moveDirection * bulletForce[1] * GameManager.instance.deltaTime);
		}
	}

	IEnumerator HitFlash(){
		meshRend.material = materials [0];
		GameManager.instance.deltaTimeScalar = hitTimeScale;
		yield return new WaitForSeconds (flashPeriod);
		GameManager.instance.deltaTimeScalar = 1.0F;
		meshRend.material = materials [1];
	}
}
