using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

	[SerializeField]
	private float dieTime = 0.16f;
	
	void OnEnable () {
		StartCoroutine (CountToDeath ());
	}
	
	private IEnumerator CountToDeath () {
		yield return new WaitForSeconds (dieTime);
		gameObject.SetActive (false);
	}
}
