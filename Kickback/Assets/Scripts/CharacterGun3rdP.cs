using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class CharacterGun3rdP : MonoBehaviour {

	public	float	rateOfFire 		= 0.32F;
	public	float	kickBackForce 	= 1.0F;

	public	float	rotAccuracy		= 1.0F;
	public	float	heightAccuracy 	= 1.0F;

	private	float	initialPitch;
	public	float	gunPitchVariance = 1.0F;

	public	float	triggerTolerance = 0.0F;

	public	AudioClip[] gunShots;

	private	Vector3	initialGunPos;
	public	float	recoilReturn = 1.0F;

	private	bool	canFire = true;

	private	bool	oldTriggerHeld;
	private	bool	triggerHeld = false;

	private Collider playerCollider;

	private	CharacterMotor characterMotor;

	private	AudioSource	audioSource;


	void Awake () {
		initialGunPos = transform.localPosition;
		audioSource = GetComponent<AudioSource> ();
		initialPitch = audioSource.pitch;
	}

	// Use this for initialization
	void Start () {
		playerCollider = transform.parent.GetComponent<Collider> ();
		characterMotor = transform.parent.GetComponent<CharacterMotor> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool newTriggerHeld = Input.GetAxis ("RightTrigger") > triggerTolerance;
		if ((!oldTriggerHeld == newTriggerHeld) && newTriggerHeld){
			triggerHeld = true;
		}

		if ((Input.GetButtonDown ("Fire3") || triggerHeld) && canFire) {

			float	randRotValue = Random.Range (-rotAccuracy, rotAccuracy);
			float	randHeightValue = Random.Range (-heightAccuracy, heightAccuracy);
			transform.localRotation = Quaternion.Euler(0.0F, randRotValue, 0.0F);
			transform.localPosition = new Vector3 (initialGunPos.x, initialGunPos.y + randHeightValue, initialGunPos.z - randHeightValue);
			BulletPool.instance.GetBullet(transform.position, transform.rotation, 1, playerCollider);
			audioSource.pitch = initialPitch + Random.Range (-gunPitchVariance, gunPitchVariance);
			audioSource.PlayOneShot(gunShots[0]);
			characterMotor.KickBack(kickBackForce);
			canFire = false;
			StartCoroutine(checkCanFire());
		}
		oldTriggerHeld = newTriggerHeld;
		triggerHeld = false;

		transform.localPosition = Vector3.Lerp (transform.localPosition, initialGunPos, GameManager.instance.deltaTime * recoilReturn);
		transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.identity, GameManager.instance.deltaTime * recoilReturn);
	}

	IEnumerator checkCanFire () {
		while (!canFire) {
			yield return new WaitForSeconds(rateOfFire);
			canFire = true;
		}
	}
}
