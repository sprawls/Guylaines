using UnityEngine;
using System.Collections;

public class GrazeOnCollision : MonoBehaviour {

	public GameObject GrazeParticles;
	public ParticleSystem GrazeSystem;


	public bool isGrinding = false;
	public AudioSource audioSource;
	// Use this for initialization
	void Awake () {
		audioSource = (AudioSource) gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isGrinding == false) {
			audioSource.volume = 0;
		} else {
			audioSource.volume = 1;
		}
	}

	void OnCollisionStay(Collision collision) {
		if(collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Floor") {
			isGrinding = true;
			////Debug.Log ("Grazed with : "  + collision.gameObject);
			Vector3 spawnPosition = collision.contacts[0].point;
			GameObject newParticles = (GameObject)Instantiate(GrazeParticles,spawnPosition, Quaternion.identity);
			newParticles.transform.parent = transform;

		}
	}

	void OnCollisionExit(Collision collision) {
		if(collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Floor") {
			isGrinding = false;
			
		}
	}

	/*
	void OnCollisionStay(Collision collision) {
		if(collision.gameObject.tag == "Obstacle") {
			//Debug.Log ("Grazed with : "  + collision.gameObject);
			Vector3 spawnPosition = collision.contacts[0].point;
			Instantiate(GrazeParticles,spawnPosition, Quaternion.identity);
			GrazeSystem.Emit(100);
		}
		
	}
	*/
}
