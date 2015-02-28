using UnityEngine;
using System.Collections;

public class KillOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Collided with : "  + collision.gameObject);
	}
}
