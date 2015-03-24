using UnityEngine;
using System.Collections;

public class Tracer : MonoBehaviour {

	[SerializeField]
	private float dieTime = 0.04f;

	void OnEnable () {
		StartCoroutine (CountToDeath ());
	}

	private IEnumerator CountToDeath () {
		yield return new WaitForSeconds (dieTime);
		gameObject.SetActive (false);
	}
}
