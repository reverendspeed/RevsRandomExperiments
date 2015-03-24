using UnityEngine;
using System.Collections;

public class CatCam : MonoBehaviour {

	public Transform catTarget;

	// (0.0f, 15.36, -9.06);
	public 	Vector3[] 	targetOffset;
	public int			targetOffsetIndex;
	
	public 	Vector4 	cameraLimits;
	public	Vector4[]	cameraLimitsArray;

	public 	float 		speed = 1.0f;

	public	int[] 		camChangeIfGreaterThan;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 target = new Vector3 (catTarget.position.x + targetOffset[targetOffsetIndex].x, 
		                              catTarget.position.y + targetOffset[targetOffsetIndex].y, 
		                              catTarget.position.z + targetOffset[targetOffsetIndex].z);

		target.x = Mathf.Clamp(target.x, cameraLimits.x, cameraLimits.y);
		target.z = Mathf.Clamp (target.z, cameraLimits.z, cameraLimits.w);

		transform.position = Vector3.Lerp (transform.position, target, speed * Time.deltaTime);

	}

	public void ChangeCameraPos(int conscriptCount) {
		if(conscriptCount >= camChangeIfGreaterThan[0]){
			targetOffsetIndex 	= 0;
		}
		if(conscriptCount >= camChangeIfGreaterThan[1]){
			targetOffsetIndex	= 1;
		}
		if(conscriptCount >= camChangeIfGreaterThan[2]){
			targetOffsetIndex	= 2;
		}
		if(conscriptCount >= camChangeIfGreaterThan[3]){
			targetOffsetIndex	= 3;
		}
	}
}
