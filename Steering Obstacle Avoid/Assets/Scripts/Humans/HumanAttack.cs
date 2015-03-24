using UnityEngine;
using System.Collections;

public class HumanAttack : MonoBehaviour {

	[SerializeField]
	private Material[]	armedMats;
	MeshRenderer		meshRenderer;

	public enum Armed{
		unarmed,
		handgun,
		uzi,
		grenadeLauncher
	}

	public	Armed armed = Armed.unarmed;

	public 	int	damageHandGun			= 1;
	public	int	damageUzi				= 3;
	public	int	damageGrenadeLauncher	= 5;

	private	bool canFireGrenadeLauncher	= true;
	public	float	GrenadeReloadTime	= 3.0f;

	public	float	uziFireRate			= 0.32f;

	public	GameObject prefabGrenade;

	void OnEnable () {
		armed			= Armed.unarmed;
	}

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	public	void Shoot (Vector3 eyePos, Vector3 target, HumanHealth humanHealth) {
		switch(armed){
		case Armed.unarmed:
			Debug.LogWarning ("How in the holy hell is this triggering? Should not be able to reach unarmed in this script!");
			break;
		case Armed.handgun:
			// Debug.Log ("Firing handgun! Bang! Bang!");
			TracerPool.instance.PlaceTracer(eyePos,target);
			humanHealth.TakeDamage (damageHandGun);
			break;
		case Armed.uzi:
			// Debug.Log ("Firing uzi! BRRRRAAAPPP!");
			StartCoroutine(UziBurst(eyePos,target));
			humanHealth.TakeDamage(damageUzi);
			break;
		case Armed.grenadeLauncher:
			if (canFireGrenadeLauncher){
				// Debug.Log ("Firing grenade launcher! Ponk... BABOOOM!");
				canFireGrenadeLauncher = false;
				StartCoroutine (GrenadeAndReload());
			}
			break;
		}
	}

	public	void ArmYourself(HumanInfection.InfectionState infectionState){
		int alteredInfectionState = (int)infectionState - 1;
		if (alteredInfectionState < 0) alteredInfectionState = 0;
		meshRenderer.material = armedMats [alteredInfectionState];
	}

	IEnumerator UziBurst(Vector3 eyePos, Vector3 target){
		TracerPool.instance.PlaceTracer (eyePos, target);
		yield return new WaitForSeconds (uziFireRate);
		TracerPool.instance.PlaceTracer (eyePos, target);
		yield return new WaitForSeconds (uziFireRate);
		TracerPool.instance.PlaceTracer (eyePos, target);
		yield return new WaitForSeconds (uziFireRate);
	}

	IEnumerator GrenadeAndReload (){
		yield return new WaitForSeconds (GrenadeReloadTime);
		canFireGrenadeLauncher = true;

	}
}
