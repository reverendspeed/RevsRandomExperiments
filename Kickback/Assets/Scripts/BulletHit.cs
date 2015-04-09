using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {

	public	Material[] 	materials;
	public	float		flashPeriod = 0.1F;

	private	MeshRenderer	meshRend;

	void Awake () {
		meshRend = GetComponent<MeshRenderer> ();
		materials[1] = meshRend.material;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision other) {
		if (other.transform.CompareTag ("Bullet01")) {
			Debug.Log ("Hit by bullet");
			StartCoroutine (HitFlash());
		}
	}

	IEnumerator HitFlash(){
		Debug.Log ("Assigning material 1");
		meshRend.material = materials [0];
		yield return new WaitForSeconds (flashPeriod);
		Debug.Log ("Assigning material 0");
		meshRend.material = materials [1];
	}
}
