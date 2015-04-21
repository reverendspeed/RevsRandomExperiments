using UnityEngine;
using System.Collections;

public class PlayerWorldHudFollow : MonoBehaviour {

	private	Transform	player;
	public	float		speed = 1.0F;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, player.position + Vector3.up * 0.1F, speed * Time.deltaTime);
	}
}
