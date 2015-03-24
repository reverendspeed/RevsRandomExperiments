using UnityEngine;
using System.Collections;

public class HumanInfection : MonoBehaviour {

	private MeshRenderer	meshRenderer;
	[SerializeField]
	private	Material[] 		bodyMats;
	[SerializeField]
	private Material[]		cleanMats;

	private HumanNavigation	humanNavigation;
	private CatConscripts	catP1RedConscripts;
	private CatConscripts	catP2BluConscripts;
	private CatConscripts	catP3GrnConscripts;
	
	public enum InfectionState
	{
		Clean,
		P1Red,
		P2Blu,
		P3Grn
	};
	
	public	InfectionState 	infectionState 				= InfectionState.Clean;
	private float 			infectionTimerCatP1Red;
	private float 			infectionTimerCatP2Blu;
	private float 			infectionTimerCatP3Grn;
	[SerializeField]
	private	float 			infectionTimerCatP1RedConst	= 1.5f;
	[SerializeField]
	private float 			infectionTimerCatP2BluConst	= 1.5f;
	[SerializeField]
	private float 			infectionTimerCatP3GrnConst	= 1.5f;

	private Transform		catP1Red;
	private Transform		catP2Blu;
	private	Transform		catP3Grn;

	void Awake () {
		meshRenderer 		= GetComponentInChildren<MeshRenderer>();
		humanNavigation		= GetComponent<HumanNavigation>();
		catP1Red 			= GameObject.FindGameObjectWithTag ("catP1Red").GetComponent<Transform> ();
		catP2Blu 			= GameObject.FindGameObjectWithTag ("catP2Blu").GetComponent<Transform> ();
		catP3Grn 			= GameObject.FindGameObjectWithTag ("catP3Grn").GetComponent<Transform> ();
		catP1RedConscripts 	= catP1Red.GetComponent<CatConscripts> ();
		catP2BluConscripts 	= catP2Blu.GetComponent<CatConscripts> ();
		catP3GrnConscripts	= catP3Grn.GetComponent<CatConscripts> ();
		CleanUpHuman ();
	}

	void OnEnable () {
		CleanUpHuman ();
	}

	// Use this for initialization
//	void Start () {
//
//	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider other){
		
		if(other.CompareTag("captureZone") && infectionState == InfectionState.Clean){
			CaptureZone zoneRef = other.GetComponent<CaptureZone>();
			
			if(zoneRef.infectionState == CaptureZone.InfectionState.P1Red){
				infectionTimerCatP1Red -= Time.deltaTime;
				if (infectionTimerCatP1Red <= 0){
					catP1RedConscripts.AddConscript(humanNavigation);
					BecomeInfectedWith((int)zoneRef.infectionState);
				}
			}
			if(zoneRef.infectionState == CaptureZone.InfectionState.P2Blu){
				infectionTimerCatP2Blu -= Time.deltaTime;
				if (infectionTimerCatP2Blu <= 0){
					catP2BluConscripts.AddConscript(humanNavigation);
					BecomeInfectedWith((int)zoneRef.infectionState);
				}
			}
			if(zoneRef.infectionState == CaptureZone.InfectionState.P3Grn){
				infectionTimerCatP3Grn -= Time.deltaTime;
				if (infectionTimerCatP3Grn <= 0){
					catP3GrnConscripts.AddConscript(humanNavigation);
					BecomeInfectedWith((int)zoneRef.infectionState);
				}
			}
		}
	}

	void OnDisable () {

	}

	private void BecomeInfectedWith(int infectionType){
		// Debug.Log ("Is cat " + infectionType);
		if(bodyMats.Length > 0){
			meshRenderer.material = bodyMats[infectionType];
		}else{
			Debug.LogWarning("No materials in the bodyMats array!");
		}
		infectionState = (InfectionState)infectionType;
	}

	private void CleanUpHuman(){
		infectionState 			= InfectionState.Clean;
		infectionTimerCatP1Red	= infectionTimerCatP1RedConst;
		infectionTimerCatP2Blu	= infectionTimerCatP2BluConst;
		infectionTimerCatP3Grn	= infectionTimerCatP3GrnConst;
		if(cleanMats.Length > 0){
			int randomMat			= Random.Range(0,cleanMats.Length);
			meshRenderer.material	= cleanMats [randomMat];
		}else{
			Debug.LogWarning("No materials in the cleanMats array!");
		}
	}
}
