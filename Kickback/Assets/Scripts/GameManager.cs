using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public	static	GameManager instance = null;
	[HideInInspector]
	public	float	deltaTime;
	public	float	deltaTimeScalar = 1.0F;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime = Time.deltaTime * deltaTimeScalar;
	}
}
