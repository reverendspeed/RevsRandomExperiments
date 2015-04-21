using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public	int	healthMax 	= 100;
	public	int	healthStart	= 100;
	public	int	healthNow 	= 100;
	public	float healthForBar;

	private	Image	healthBar;

	public	Material[] 		materials;
	public	float			flashPeriod = 0.1F;
	public	float			hitTimeScale = 1.0F;

	public	AudioClip		painSound01;
	private	AudioSource		audSource;
	private	float			audPitchOrig;
	private	float			audVolOrig;
	public	float			audPitchVariance = 0.03F;
	public	float			audVolVariance = 0.5F;
	
	private	MeshRenderer	meshRend;

	private	bool			playerDying= false;
	private	bool			playerDead = false;

	private	Transform		spawnPoint;

	void Awake () {
		meshRend = transform.FindChild ("Body").GetComponent<MeshRenderer> ();
		materials[1] = meshRend.material;

		audSource = GetComponent<AudioSource> ();
		audPitchOrig = audSource.pitch;
		audVolOrig = audSource.volume;
	}
	
	// Use this for initialization
	void Start () {
		healthBar = GameObject.FindGameObjectWithTag ("LeftStatusBar").GetComponent<Image>();
		healthForBar = healthBar.fillAmount;
		spawnPoint = GameObject.FindGameObjectWithTag ("P1Spawn").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoseHealth (int loss) {
		if (!playerDying || !playerDead) {
			healthNow -= loss;
			StartCoroutine (HitFlash ());
			audSource.pitch = Random.Range (-audPitchVariance + audPitchOrig, audPitchVariance + audPitchOrig);
			audSource.volume = Random.Range (-audVolVariance + audVolOrig, audVolOrig);
			audSource.PlayOneShot (painSound01);
			// audSource.pitch = audPitchOrig;
			HealthCheck ();
		}
	}

	public void GainHealth (int gain) {
		if (!playerDying || !playerDead) {
			healthNow += gain;
			HealthCheck ();
		}
	}

	void HealthCheck () {
		healthNow = Mathf.Clamp (healthNow, 0, healthMax);
		healthBar.fillAmount = healthForBar = ((float)healthNow / 100);
		if (healthNow <= 0 && !playerDying) {
			HealthDying ();
		}
	}

	void HealthDying (){
		playerDying = true;
		// Play death audio, animation, etc
		HealthDead ();
	}

	void HealthDead (){
		playerDead = true;
		Debug.Log ("Player is dead.");
		HealthReset ();
	}

	public	void HealthReset (){
		healthNow = healthStart;
		playerDying = false;
		playerDead = false;
		meshRend.material = materials [1];
		HealthCheck ();
		transform.position = spawnPoint.position;
	}

	IEnumerator HitFlash(){
		meshRend.material = materials [0];
		GameManager.instance.deltaTimeScalar = hitTimeScale;
		yield return new WaitForSeconds (flashPeriod);
		GameManager.instance.deltaTimeScalar = 1.0F;
		meshRend.material = materials [1];
	}
}
