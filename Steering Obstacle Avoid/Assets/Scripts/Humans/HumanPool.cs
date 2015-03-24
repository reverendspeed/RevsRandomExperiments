using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPool : MonoBehaviour {

	private GameObject[] 		spawnPoints;
	private Vector3 			spawnPosition;
	private Quaternion 			spawnRotation;

	public	List<GameObject>	spawnedHumans			= new List<GameObject>();
	public	GameObject			humanPrefab;

	public	int					humanPopLimit			= 4;
	public	float				populationRate			= 4.0f;
	public	int					populateAmount			= 4;
	public	int					prevPopulateIndex		= 0;

	private	List<HumanVision>	humanVisions			= new List<HumanVision>();
	public	float				visionCheckRate 		= 1.0f;
	public	int					prevHumanVisionIndex	= 0;
	public	int					visionPerLoopMax		= 5;
	public	int					visionPerLoopMin		= 2;
	private	int					visionPerLoopCurrent	= 3;

	public	int totalHumanVisionToCheckThisLoop;

	void Awake () {

		spawnPoints = GameObject.FindGameObjectsWithTag ("humanSpawnPoint");
		
		for(int i = 0; i <= humanPopLimit; i++){
			GameObject newHuman 		= Instantiate(humanPrefab) as GameObject;
			newHuman.transform.parent	= this.transform;
			spawnedHumans.Add(newHuman);
			humanVisions.Add(newHuman.transform.GetComponent<HumanVision>());
			newHuman.SetActive(false);
		}

		StartCoroutine(SpawnLoop());
		StartCoroutine(VisionCheckPrompt());
	}

	void OnEnable () {

	}

	IEnumerator SpawnLoop () {
		while (enabled){
			yield return new WaitForSeconds (populationRate);
			if (populateAmount > spawnedHumans.Count){
				Debug.LogWarning("Populate amount per SpawnLoop exceeds humans.Count! Setting populateAmount to humans.Count -1");
				populateAmount = spawnedHumans.Count - 1;
			}
			int currentPopulateAmount = populateAmount;
			for(int i = prevPopulateIndex; currentPopulateAmount > 0; i++){
				if (i > spawnedHumans.Count - 1) i = 0;
				prevPopulateIndex = i;
				currentPopulateAmount--;
				if(!spawnedHumans[i].activeInHierarchy){
					SelectRandomSpawn ();
					spawnedHumans[i].transform.position = spawnPosition;
					spawnedHumans[i].transform.rotation = spawnRotation;
					spawnedHumans[i].SetActive(true);
				}
			}
		}
	}

	IEnumerator VisionCheckPrompt () {

		while (enabled) {
			yield return new WaitForSeconds(visionCheckRate);

			visionPerLoopCurrent = Random.Range(visionPerLoopMin, visionPerLoopMax);
			// Debug.Log ("Total human visions to check this loop is: " + visionPerLoopCurrent);
			// Debug.Log ("Total spawned humans is: " + spawnedHumans.Count);
			if (visionPerLoopCurrent > spawnedHumans.Count){
				visionPerLoopCurrent = spawnedHumans.Count - 1;
			}

			totalHumanVisionToCheckThisLoop = visionPerLoopCurrent;
			// Debug.Log ("totalHumanVisionToCheckThisLoop: " + totalHumanVisionToCheckThisLoop);
			for(int i = prevHumanVisionIndex; totalHumanVisionToCheckThisLoop > 0; i++){
				if(i > humanVisions.Count - 1) i = 0;
				prevHumanVisionIndex = i;
				// Debug.Log ("Previous Human Vision Index is: " + prevHumanVisionIndex);
				totalHumanVisionToCheckThisLoop--;
				// Debug.Log ("totalHumanVisionToCheckThisLoop: " + totalHumanVisionToCheckThisLoop);
				if(spawnedHumans[i].activeInHierarchy){
					humanVisions[i].InspectNearbyHumans();
				}
			}
		}
	}

	void SelectRandomSpawn () {
		int spawnPointsIndex 		= Random.Range (0,spawnPoints.Length);
		spawnPosition				= spawnPoints[spawnPointsIndex].transform.position;
		spawnRotation				= spawnPoints[spawnPointsIndex].transform.rotation;
	}
}
