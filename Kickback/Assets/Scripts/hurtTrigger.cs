using UnityEngine;
using System.Collections;

public class hurtTrigger : MonoBehaviour {

	private	PlayerHealth	playerHealth;
	private	bool			hurtPlayer 	= false;
	public	int				hurtAmount	= 1;
	public	float			hurtTimer	= 0.0F;
	public	float			hurtInterval= 1.0F;

	// Use this for initialization
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hurtPlayer) {
			hurtTimer += Time.deltaTime;
			if (hurtTimer > hurtInterval){
				playerHealth.LoseHealth (hurtAmount);
				hurtTimer = 0.0F;
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.transform.CompareTag ("Player")) {
			hurtPlayer 	= true;
			hurtTimer	= 0.0F;
			playerHealth.LoseHealth(hurtAmount);

		}
	}

	void OnTriggerExit (Collider other) {
		if (other.transform.CompareTag ("Player")) {
			hurtPlayer = false;
		}
	}
}
