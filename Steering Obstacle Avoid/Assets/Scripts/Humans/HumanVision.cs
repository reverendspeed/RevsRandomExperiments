using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanVision : MonoBehaviour {

	public 	Color 			debugRayColor 			= Color.green;
	[SerializeField]
	private	float 			fieldOfViewAngle 		= 110f;		// Number of degrees, centred on forward, for the enemy viewcone.
	private	float			halfFieldOfViewAngle;
	private const float		eyeHeight				= 1.60f;
	[SerializeField]
	private	LayerMask		humanLayerMask; 					// So vision raycast only detects humans, set in inspector for your pleasure!

	private HumanInfection	myInfection;
	public	List<HumanInfection>	nearbyHumans	= new List<HumanInfection>();

	private	HumanAttack		humanAttack;

	void Awake () {
		myInfection				= GetComponent<HumanInfection>();
		humanAttack				= GetComponent<HumanAttack>();
		halfFieldOfViewAngle	= fieldOfViewAngle * 0.5f;
	}

	void OnTriggerEnter (Collider other){
		if(other.CompareTag("human")){
			nearbyHumans.Add(other.GetComponent<HumanInfection>());
		}
	}

	void OnTriggerExit (Collider other){
		if(other.CompareTag("human")){
			nearbyHumans.Remove (other.GetComponent<HumanInfection>()); // Really unsure if this is removing the correct corresponding HumanInfection
		}
	}

	public void InspectNearbyHumans() {
		// Debug.Log ("Running TestVision");
		if(humanAttack.armed == HumanAttack.Armed.unarmed){
			return;
		}else {
			for(int i = 0; i < nearbyHumans.Count; i++){
					
				RaycastHit 	hit;
				Vector3 	humanToTarget 	= nearbyHumans[i].transform.position - transform.position;
				Vector3 	eyePos	 		= new Vector3 (transform.position.x, transform.position.y + eyeHeight, transform.position.z);
				
				// Create a vector from the enemy to the player and store the angle between it and forward.
				float enemyRelativeAngle	= Vector3.Angle(humanToTarget, transform.forward);
				
				// If the angle between forward and where the player is, is less than half the angle of view...
				if(IsWithinFieldOfView(enemyRelativeAngle))	{
					if(Physics.Raycast(eyePos, humanToTarget, out hit, humanLayerMask) && hit.transform.CompareTag("human")){
						HumanInfection.InfectionState otherInfection = hit.transform.GetComponent<HumanInfection>().infectionState;
						if(otherInfection != myInfection.infectionState && otherInfection != HumanInfection.InfectionState.Clean){
							humanAttack.Shoot (eyePos, hit.point, hit.transform.GetComponent<HumanHealth>());
						} 
					}
				}
			}
		}
	}

	/* Stick an else in here which if THIS human is clean and the human in view is NOT && is !UNARMED, 
	 * then setdirection to transform.position - other.transform.position normalised * run distance (evac zone? Cower state?)
	 */


	bool IsWithinFieldOfView (float enemyRelativeAngle) {
		return enemyRelativeAngle < halfFieldOfViewAngle;
	}
}
