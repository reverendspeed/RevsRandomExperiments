using UnityEngine;
using System.Collections;

public class CatAIGrnNav : MonoBehaviour {

	private NavMeshAgent 	agent;
	[SerializeField]
	private	float			navDestinRadius	 		= 1.5f;
	[SerializeField]
	private float			destinCheckRate			= 2.0f;
	[SerializeField]
	private	float			destinWait				= 2.0f;
	private GameObject[] 	shopWaypoints;
//	private Transform		catP1Red;
//	private Transform		catP2Blu;
//	private	Transform		catP3Grn;

	void Awake () {

		agent 					= GetComponent<NavMeshAgent>();
		shopWaypoints 			= GameObject.FindGameObjectsWithTag ("waypointShop");
		
//		catP1Red 				= GameObject.FindGameObjectWithTag ("catP1Red").GetComponent<Transform> ();
//		catP2Blu 				= GameObject.FindGameObjectWithTag ("catP2Blu").GetComponent<Transform> ();
//		catP3Grn 				= GameObject.FindGameObjectWithTag ("catP3Grn").GetComponent<Transform> ();
		
		RandomDestination ();
		StartCoroutine (DestinationChecker ());
	}

	void Enabled () {

	}

	// Use this for initialization
//	void Start () {
//
//	}
	
	// Update is called once per frame
	void Update () {
	

	}

	private IEnumerator DestinationChecker () {
		while (enabled){
			yield return new WaitForSeconds(destinCheckRate);

			if (agent.remainingDistance < navDestinRadius){
				yield return new WaitForSeconds (destinWait);
				RandomDestination ();
			}
		}
	}

	private void RandomDestination(){
		int randomIndex 		= Random.Range (0, shopWaypoints.Length);
		Vector3 newDestination 	= shopWaypoints[randomIndex].transform.position;
		agent.SetDestination (newDestination);
	}
}
