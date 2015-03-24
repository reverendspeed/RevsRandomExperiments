using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerPool : MonoBehaviour {

	private	static	TracerPool			_instance;
	public	static	TracerPool			instance{
		get {
			if (_instance == null) _instance = GameObject.FindObjectOfType<TracerPool>();
			return _instance;
		}
	}

	private List<GameObject> 	tracerList		= new List<GameObject>();
	private List<LineRenderer>	tracerLines		= new List<LineRenderer>();
	public	GameObject			tracerPrefab;
	public	float				tracerPopLimit	=	12.0f;

	public	List<GameObject>	flashList		= new List<GameObject>();
	public	GameObject			flashPrefab;

	public	AudioClip			gunshot;
	private	AudioSource			audioSource;

	// Use this for initialization
	void Start () {

		for (int i = 0; i <= tracerPopLimit; i++){
			GameObject tracerClone = Instantiate(tracerPrefab) as GameObject;
			tracerClone.gameObject.transform.parent = this.transform;
			tracerList.Add(tracerClone);
			tracerLines.Add(tracerClone.GetComponent<LineRenderer>());
			tracerClone.gameObject.SetActive (false);

//			Debug.Log ("Instantiating flashPrefab.");
			GameObject flashClone = Instantiate(flashPrefab) as GameObject;
			flashClone.gameObject.transform.parent = this.transform;
			flashList.Add(flashClone);
			flashClone.gameObject.SetActive (false);
		}

		audioSource = GetComponent<AudioSource> ();
	}

	public void PlaceTracer (Vector3 startPos, Vector3 endPos){
		// Debug.Log ("Placing Tracer!");
		for (int i = 0; i < tracerList.Count; i++){
			if (!tracerList[i].gameObject.activeInHierarchy){
				tracerList[i].gameObject.SetActive(true);
				tracerList[i].transform.position = startPos;
				tracerLines[i].SetPosition(0, startPos);
				tracerLines[i].SetPosition(1, endPos);

				flashList[i].gameObject.SetActive(true);
				flashList[i].transform.position = startPos;

				// Debug.Log ("Placed tracer at: " + tracerList[i].transform.position);
				audioSource.Play ();
				return;
			}
		}
	}
}
