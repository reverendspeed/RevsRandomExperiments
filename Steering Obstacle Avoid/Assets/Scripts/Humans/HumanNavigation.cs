using UnityEngine;
using System.Collections;

public class HumanNavigation : MonoBehaviour {

	private NavMeshAgent 			agent;
	[SerializeField]
	private	float					agentDestinationRadius 	= 1.5f;
	public	float					agentSpeedMax			= 4.0f;
	public	float					agentSpeedMin			= 2.0f;
	private	float					agentSpeedRun			= 6.0f;
	private GameObject[] 			shopWaypoints;

	private	Transform				catP1RedHumanTarget;
	private	Transform				catP2BluHumanTarget;
	private	Transform				catP3GrnHumanTarget;

	public	Transform				destinationDemonstration;

	private HumanInfection			humanInfection;

	void Awake () {

		agent 					= GetComponent<NavMeshAgent>();
		shopWaypoints 			= GameObject.FindGameObjectsWithTag ("waypointShop");

		catP1RedHumanTarget		= GameObject.Find ("catP1RedHumanTarget").GetComponent<Transform>();
		catP2BluHumanTarget		= GameObject.Find ("catP2BluHumanTarget").GetComponent<Transform>();
		catP3GrnHumanTarget		= GameObject.Find ("catP3GrnHumanTarget").GetComponent<Transform>();

		humanInfection			= GetComponent<HumanInfection>();
		
		RandomDestination ();

	}

	void Enabled () {

	}
	
	// Update is called once per frame
	void Update () {

		if (agent.remainingDistance < agentDestinationRadius){
			if(humanInfection.infectionState 		== HumanInfection.InfectionState.Clean){
				RandomDestination ();
			}
			if (humanInfection.infectionState 		== HumanInfection.InfectionState.P1Red){
				agent.SetDestination(OffsetDestination(catP1RedHumanTarget.position));
			}else if (humanInfection.infectionState == HumanInfection.InfectionState.P2Blu){
				agent.SetDestination(OffsetDestination(catP2BluHumanTarget.position));
			}else if (humanInfection.infectionState == HumanInfection.InfectionState.P3Grn){
				agent.SetDestination(OffsetDestination(catP3GrnHumanTarget.position));
			}
		}
		if (humanInfection.infectionState		!= HumanInfection.InfectionState.Clean) SetInfectedToRun();
	}

	private void RandomDestination(){
		agent.speed 			= Random.Range (agentSpeedMin, agentSpeedMax);
		int randomIndex 		= Random.Range (0, shopWaypoints.Length);
		Vector3 newDestination 	= shopWaypoints[randomIndex].transform.position;
		agent.SetDestination (newDestination);
	}

	private Vector3 OffsetDestination(Vector3 originalDestination){
		Vector3 alteredDestination	= new Vector3 (	originalDestination.x + (Random.Range (-agentDestinationRadius, agentDestinationRadius)),
		                                     		originalDestination.y,
		                                     		originalDestination.z + (Random.Range (-agentDestinationRadius, agentDestinationRadius)));
		return alteredDestination;
	}

	private void SetInfectedToRun () {
		if (agent.speed != agentSpeedRun){
			agent.speed = agentSpeedRun;
		}
	}
}
