using UnityEngine;
using System.Collections;

public class PlatformRepeat : MonoBehaviour {
	void Update() {
		transform.position = new Vector3(Mathf.Repeat(Time.time, 3), transform.position.y, transform.position.z);
	}
}
