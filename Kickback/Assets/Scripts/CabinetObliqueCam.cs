using UnityEngine;
using System.Collections;

public class CabinetObliqueCam : MonoBehaviour {
	public	float		playerLookAhead = 1.0F;
	public	float		cameraMoveSpeed = 1.0F;
	public	float		cameraRotSpeed = 1.0F;
	private	Transform	playerTransform;
	private	Vector3		cameraOffset;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		cameraOffset	= transform.position - playerTransform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp(transform.position,
		                                  cameraOffset + (playerTransform.position + playerTransform.forward * playerLookAhead),
		                                  cameraMoveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, 
		                                      Quaternion.LookRotation ((playerTransform.position + playerTransform.forward * playerLookAhead) - transform.position),
		                                      cameraRotSpeed * Time.deltaTime);
	}
}
