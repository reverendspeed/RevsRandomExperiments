using UnityEngine;
using System.Collections;

public class BillboardRotator : MonoBehaviour {

	private	Camera		cam;
	public	Vector3		startScale;
	public	Vector3		flashScale;

	void Awake () {
		startScale = transform.localScale;
		flashScale = startScale * 2;
	}

	void OnEnable () {
		transform.localScale = flashScale;
		StartCoroutine ("NormalScale");
	}

	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		transform.rotation = Quaternion.LookRotation (transform.position - cam.transform.position);
	}

	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (transform.position - cam.transform.position);
	}

	IEnumerator NormalScale() {
		yield return new WaitForSeconds (0.025F);
		transform.localScale = startScale;
	}
}
