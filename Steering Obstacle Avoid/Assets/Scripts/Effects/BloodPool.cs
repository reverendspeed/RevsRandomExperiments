using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodPool : MonoBehaviour {

	private static	BloodPool _instance;

	public 	static	BloodPool instance{
		get{
			if(_instance == null) _instance = GameObject.FindObjectOfType<BloodPool>();
			return _instance;
		}
	}

	public	GameObject				bloodPrefab;
	public	int						bloodPopLimit 	= 48;
	private	List<GameObject> 		bloodList 		= new List<GameObject>();
	private	List<ParticleSystem>	bloodEmitter 	= new List<ParticleSystem>();

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < bloodPopLimit; i++){
			GameObject bloodClone 			= Instantiate (bloodPrefab) as GameObject;
			bloodClone.transform.parent 	= this.transform;
			bloodList.Add(bloodClone);
			bloodEmitter.Add(bloodClone.GetComponentInChildren <ParticleSystem>());
			bloodClone.SetActive(false);
		}
	}

	public void PlaceBlood (Vector3 victimPos, Quaternion victimRot) {
		for (int i = 0; i < bloodList.Count; i++){
			if (!bloodList[i].activeInHierarchy) {
				bloodList[i].SetActive(true);
				bloodList[i].transform.position = victimPos;
				bloodList[i].transform.rotation = victimRot;
				bloodEmitter[i].Play(true);
				return;
			}
		}
	}
}
