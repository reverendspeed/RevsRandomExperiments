using UnityEngine;
using System.Collections;

public class GunZone : MonoBehaviour {

	public	AudioClip	toolUp;

	private int armedLength;

	[SerializeField]
	private int	percentUzi				= 60;
	[SerializeField]
	private	int	percentGrenadeLauncher	= 90;

	void OnTriggerEnter(Collider other){

		if(other.CompareTag("human")){
			HumanInfection 	humanInfection 	= other.GetComponent<HumanInfection>();
			if (humanInfection.infectionState != HumanInfection.InfectionState.Clean){
				SelectAndGiveGun (other.GetComponent<HumanAttack>(), humanInfection);
			}
		}
	}

	void SelectAndGiveGun (HumanAttack humanAttack, HumanInfection humanInfection){
		humanAttack.ArmYourself(humanInfection.infectionState);
		if(humanAttack.armed == HumanAttack.Armed.unarmed){
			AudioSource.PlayClipAtPoint (toolUp, Vector3.zero);
			int percent = Random.Range(0,100);
			if(percent < percentUzi)									humanAttack.armed = HumanAttack.Armed.handgun;
			if(percent > percentUzi && percent < percentGrenadeLauncher)humanAttack.armed = HumanAttack.Armed.uzi;
			if(percent > percentGrenadeLauncher) 						humanAttack.armed = HumanAttack.Armed.grenadeLauncher;
		}
	}
}
