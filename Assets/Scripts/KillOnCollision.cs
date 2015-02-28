using UnityEngine;
using System.Collections;

public class KillOnCollision : MonoBehaviour {

	private ShipControl shipControl;
	// Use this for initialization
	void Awake() {
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		shipControl = playerObj.GetComponentInChildren<ShipControl>();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Obstacle") {
			Debug.Log ("Collided with : "  + collision.gameObject);
			shipControl.Kill();
		}

	}
}
