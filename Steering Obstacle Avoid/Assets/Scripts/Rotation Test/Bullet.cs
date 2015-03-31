using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private	Camera	cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	void OnEnable () {
		Invoke ("Destroy", 1.0F);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (transform.position - cam.transform.position);
	}

	void Destroy () {
		gameObject.SetActive (false);
	}

	void OnDisable () {
		CancelInvoke ();
	}
}
