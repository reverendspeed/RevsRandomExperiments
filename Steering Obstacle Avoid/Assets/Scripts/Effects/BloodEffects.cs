using UnityEngine;
using System.Collections;

public class BloodEffects : MonoBehaviour {

	private	ParticleSystem particleSystemLocal;

	[SerializeField]
	private float dieTime = 0.16f;

	void Awake () {
		particleSystemLocal = GetComponentInChildren<ParticleSystem> ();
		particleSystemLocal.Stop (true);
	}

	void OnEnable () {
		StartCoroutine (CountToDeath ());
	}
	
	private IEnumerator CountToDeath () {
		yield return new WaitForSeconds (dieTime);
		particleSystemLocal.Stop (true);
		gameObject.SetActive (false);
	}
}
