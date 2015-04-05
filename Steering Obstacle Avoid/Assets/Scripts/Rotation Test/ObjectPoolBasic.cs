using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolBasic : MonoBehaviour {

	public	List<GameObject>	spawnedBullets			= new List<GameObject>();
	public	GameObject			bulletPrefab;

	public	int					bulletLimit				= 4;
//	public	float				populationRate			= 4.0f;
//	public	int					populateAmount			= 4;
//	public	int					prevPopulateIndex		= 0;

	void Awake () {
//		Instantiate ();
		for (int i = 0; i <= bulletLimit; i++) {
			Instantiate();
		}
	}

	void Instantiate () {
		GameObject newBullet 		= Instantiate(bulletPrefab) as GameObject;
		newBullet.transform.parent	= this.transform;
		spawnedBullets.Add(newBullet);
		newBullet.SetActive(false);
	}

	void BulletSpawn (Vector3 bulletPosition, Quaternion bulletRotation) {

		for (int i = 0; i < spawnedBullets.Count; i++) {
			if(!spawnedBullets[i].activeInHierarchy){
				spawnedBullets[i].transform.position = bulletPosition;
				spawnedBullets[i].transform.rotation = bulletRotation;
				spawnedBullets[i].SetActive(true);
				return;
			}
		}
		Instantiate();
		spawnedBullets.FindLast ().transform.position = bulletPosition;
		spawnedBullets.FindLast ().transform.rotation = bulletRotation;
		spawnedBullets.FindLast ().SetActive (true);
//		if (populateAmount > spawnedBullets.Count){
//			Debug.LogWarning("Populate amount per SpawnLoop exceeds humans.Count! Setting populateAmount to humans.Count -1");
//			populateAmount = spawnedBullets.Count - 1;
//		}
//		int currentPopulateAmount = populateAmount;
//		for(int i = prevPopulateIndex; currentPopulateAmount > 0; i++){
//			if (i > spawnedBullets.Count - 1) i = 0;
//			prevPopulateIndex = i;
//			currentPopulateAmount--;
//			if(!spawnedBullets[i].activeInHierarchy){;
//				spawnedBullets[i].transform.position = bulletPosition;
//				spawnedBullets[i].transform.rotation = bulletRotation;
//				spawnedBullets[i].SetActive(true);
//			}
//		}
	}
}
