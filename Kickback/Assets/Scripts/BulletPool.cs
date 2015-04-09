using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {

	static public BulletPool 	instance = null;

	public	PlasmaBall			bulletPrefab;
	public	List<PlasmaBall> 	pool;
	
	public	Material[] 			matArray;
	public	int					totalBulletCount = 20;


	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this);
		}

		DontDestroyOnLoad (this);

		for (int i = 0; i < totalBulletCount; i++) {
			InitialiseBullet ();
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitialiseBullet () {
		PlasmaBall tempBullet = GameObject.Instantiate (bulletPrefab) as PlasmaBall;
		pool.Add (tempBullet);
		tempBullet.transform.SetParent (this.transform);
		tempBullet.gameObject.SetActive (false);
	}

	public	void	GetBullet (Vector3 position, Quaternion rotation, int	materialType, Collider collidr){
		for (int i = 0; i < pool.Count; i++) {
			if(!pool[i].isActiveAndEnabled){
				SetupBullet(pool[i], position, rotation, materialType, collidr);
				return;
			}
		}
		totalBulletCount++;
		InitialiseBullet();
		SetupBullet(pool[pool.Count -1], position, rotation, materialType, collidr);
	}

	void SetupBullet(PlasmaBall currentBullet, Vector3 position, Quaternion rotation, int materialType, Collider collidr){
		currentBullet.gameObject.transform.position = position;
		currentBullet.gameObject.transform.rotation = rotation;
		currentBullet.meshRenderer.materials[0] = matArray[materialType];
		currentBullet.gameObject.SetActive (true);
		Physics.IgnoreCollision (currentBullet.collidr, collidr);
	}
}
