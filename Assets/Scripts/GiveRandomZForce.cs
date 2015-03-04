using UnityEngine;
using System.Collections;

public class GiveRandomZForce : MonoBehaviour {

	private ShipControl shipControl;
	private bool boostGiven = false;
	private Vector3 objForce;

	//Use this for initialization
	void Awake() {
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		shipControl = playerObj.GetComponentInChildren<ShipControl>();
	}

	void Start () {
		float randX = Random.Range (-150,150)*shipControl.forwardSpeed;
		float randY = Random.Range (150,300)*shipControl.forwardSpeed;
		float randZ = Random.Range (500,1100)*shipControl.forwardSpeed;
		objForce = new Vector3(randX,randY,randZ);
		GetComponent<Rigidbody>().isKinematic = true;

	}
	
	// Update is called once per frame
	void Update () {
		if(boostGiven == false && shipControl.slowMoEnded == true) {
			boostGiven = true;
			GiveForce();
		}
	}

	void GiveForce() {
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().AddForce(objForce);
	}
}
