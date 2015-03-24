using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureZone : MonoBehaviour {

	public	AudioClip[]		miaows;

	[SerializeField]
	private	Material[]		zoneMats;
	private MeshRenderer	meshRenderer;

	public	List<Transform> potentialInfectors = new List<Transform>();

	public enum InfectionState
		{
		Clean,
		P1Red,
		P2Blu,
		P3Grn
		};

	public	InfectionState infectionState = InfectionState.Clean;
	[SerializeField]
	private float infectionTimer = 5.0f;
	[SerializeField]
	private float infectionTimerReset = 5.0f;

	// Use this for initialization
	void Start () {
	
		meshRenderer = GetComponent<MeshRenderer>();
		StartCoroutine (InfectionLoop ());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator InfectionLoop() {
		// Debug.Log ("Infection loop started...");
		while(enabled){
			if(potentialInfectors.Count > 0){
				for (int i = 0; i < potentialInfectors.Count; i++){
					CheckAndInfect (i);
				}
			}
			yield return null;
		}
	}

	void OnTriggerEnter (Collider other) {
		potentialInfectors.Add (other.transform);
	}

	void OnTriggerExit (Collider other) {
		potentialInfectors.Remove (other.transform);
	}

	private void CleanUp (){
		infectionTimer -= Time.deltaTime;
		
		if(infectionTimer <= 0){
			
//			Debug.Log ("Is clean!");
			meshRenderer.material = zoneMats[0];
			infectionState = InfectionState.Clean;
			infectionTimer = infectionTimerReset;
		}
	}

	private void BecomeInfectedWith(int infectionType){
//		Debug.Log ("Infected with cat " + infectionType);
		AudioSource.PlayClipAtPoint (miaows [Random.Range (0, miaows.Length)], Vector3.zero);
		meshRenderer.material = zoneMats[infectionType];
		infectionState = (InfectionState)infectionType;
		infectionTimer = infectionTimerReset;
	}

	private void CheckAndInfect(int currentInfector){
		if (infectionState == InfectionState.Clean){
			if(potentialInfectors[currentInfector].CompareTag("catP1Red")){
				infectionTimer -= Time.deltaTime;
				
				if (infectionTimer <= 0){
					BecomeInfectedWith(1);
				}
				/* Below is probably faster, but everything would run on separate
							 * timers, meaning that you wouldn't get a benefit to capturing a
							 * point that your opponent had half-capped and had to abandon.
							 */
				//							yield return new WaitForSeconds(infectionTimer);
				//							BecomeInfectedWith(1);
			}
			if(potentialInfectors[currentInfector].CompareTag ("catP2Blu")){
				infectionTimer -= Time.deltaTime;
				
				if(infectionTimer <= 0){
					BecomeInfectedWith(2);
				}
			}
			if(potentialInfectors[currentInfector].CompareTag ("catP3Grn")){
				infectionTimer -= Time.deltaTime;
				
				if(infectionTimer <= 0){
					BecomeInfectedWith(3);
				}
			}
		}else if (infectionState == InfectionState.P1Red && (potentialInfectors[currentInfector].CompareTag("catP2Blu") ||
		                                                     potentialInfectors[currentInfector].CompareTag("catP3Grn"))){
			CleanUp();
		}else if (infectionState == InfectionState.P2Blu && (potentialInfectors[currentInfector].CompareTag("catP1Red") ||
		                                                     potentialInfectors[currentInfector].CompareTag("catP3Grn"))){
			CleanUp();
		}else if (infectionState == InfectionState.P3Grn && (potentialInfectors[currentInfector].CompareTag("catP1Red") ||
		                                                     potentialInfectors[currentInfector].CompareTag("catP2Blu"))){
			CleanUp();
		}
	}
}
