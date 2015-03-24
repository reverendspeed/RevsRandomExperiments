using UnityEngine;
using System.Collections;

public class HumanHealth : MonoBehaviour {

	public	AudioClip[]	screams;
	private	AudioSource audioSource;

	[SerializeField]
	private	int				humanHealth				= 6;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}

	 // Update is called once per frame
//	void Update () {
//		if(Input.GetKeyDown(KeyCode.Space)){
//			audioSource.clip = screams[Random.Range (0, screams.Length)];
//			audioSource.Play ();
//		}
//	}

	public void TakeDamage (int damageAmount) {
		humanHealth -= damageAmount;
		BloodPool.instance.PlaceBlood (transform.position, transform.rotation);
		// Debug.Log ("Ow! Took: " + damageAmount + " damage.");
		if(humanHealth <= 0) Die ();
	}

	void Die () {
		audioSource.clip = screams[Random.Range (0, screams.Length)];
		AudioSource.PlayClipAtPoint (audioSource.clip, Vector3.zero);
		// audioSource.Play ();
		Debug.Log ("Scream!");
		gameObject.SetActive (false);
	}
}
