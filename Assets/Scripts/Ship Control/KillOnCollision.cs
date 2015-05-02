using UnityEngine;
using System.Collections;

public class KillOnCollision : MonoBehaviour {

	public GameObject DeathParticles;
	private ShipControl shipControl;
	// Use this for initialization
	void Awake() {
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		shipControl = playerObj.GetComponentInChildren<ShipControl>();
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Obstacle") {
			shipControl.Kill();
			Instantiate(DeathParticles,transform.position, Quaternion.identity);
		}
	}
}
