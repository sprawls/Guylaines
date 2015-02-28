using UnityEngine;
using System.Collections;

public class GrazeOnCollision : MonoBehaviour {

	public GameObject GrazeParticles;
	public ParticleSystem GrazeSystem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision collision) {
		if(collision.gameObject.tag == "Obstacle") {
			Debug.Log ("Grazed with : "  + collision.gameObject);
			Vector3 spawnPosition = collision.contacts[0].point;
			GameObject newParticles = (GameObject)Instantiate(GrazeParticles,spawnPosition, Quaternion.identity);
			newParticles.transform.parent = transform;

		}
	}

	/*
	void OnCollisionStay(Collision collision) {
		if(collision.gameObject.tag == "Obstacle") {
			Debug.Log ("Grazed with : "  + collision.gameObject);
			Vector3 spawnPosition = collision.contacts[0].point;
			Instantiate(GrazeParticles,spawnPosition, Quaternion.identity);
			GrazeSystem.Emit(100);
		}
		
	}
	*/
}
