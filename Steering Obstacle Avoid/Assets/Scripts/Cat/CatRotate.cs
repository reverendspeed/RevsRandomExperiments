using UnityEngine;
using System.Collections;

public class CatRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Rotate (Vector3 moveDirection) {
	
		Quaternion rotation = Quaternion.LookRotation(moveDirection);
		transform.rotation = rotation;

	}
}
