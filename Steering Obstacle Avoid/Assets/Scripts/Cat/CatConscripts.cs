using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatConscripts : MonoBehaviour {

	public	AudioClip[]		purrs;

	public List<HumanNavigation>	conscriptsFollowing	= new List<HumanNavigation>();
	public List<HumanNavigation>	conscriptsGuarding	= new List<HumanNavigation>();
	[SerializeField]
	private CatCam	catCam; // Set up from inspector - actually the easiest way!
	public	bool	updateCatCam = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddConscript (HumanNavigation conscript) {
//		Debug.Log ("Adding a conscript...");
		conscriptsFollowing.Add (conscript);
		AudioSource.PlayClipAtPoint (purrs [Random.Range (0, purrs.Length)], Vector3.zero);
		CameraDistanceTest ();
	}

	private	void OrderConscriptGather () {

	}

	private void OrderConscriptGuard () {

	}

	private void CameraDistanceTest() {
		if(updateCatCam) catCam.ChangeCameraPos (conscriptsFollowing.Count + conscriptsGuarding.Count);
	}
}
