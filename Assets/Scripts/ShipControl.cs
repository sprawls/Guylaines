using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {

	////////////////////////// General //////////////////////////
	public Transform modelAndCam;
	public Transform model;
	public MoveCameraFromSpeed cameraScript;
	public bool isDead = false;
	////////////////////////// Forward Speed //////////////////////////
	public float forwardSpeed;

    private float speedIncrementPerLevel = 0.01f;
	private float speedIncrementPerSecond = 0.0f;
	private float startSpeed = 2.0f;
	////////////////////////// Side Speed //////////////////////////
	public float sideSpeed; //lerp from 0 to maxSideSpeed using curSideSpeedMultiplier as "t"
	public float tiltAngle;
	public float control; //change in sideSpeed per second( 1 means it takes 1sec to reach max sidespeed; 2 means it takes 0.5sec)
	public float stabilizingControlratio = 0.5f; //When no key is pressed, ratio of the control the ship uses to stabilize
	public float sideSpeedLimit = 0.5f;
	public float maxTiltAngle = 5;
	private float curSideSpeedMultiplier = 0; //Variable between 0 and 1 dictating speed
	private float deadTiltZone = 0.025f; // if sidespeed is lower than this, it equals 0

	////////////////////////// Death //////////////////////////
	[HideInInspector] public bool slowMoEnded = false;
	private RotatingPlatform rotatePlatform;
	private Translation translation;

	////////////////////////// Tilt on Fire //////////////////////////
	public float tiltCooldown = 3f;
	public float tiltTime = 1.5f;
	private float tiltTimeAnimation = 1f;
	public GameObject[] objectsToSuperTilt;
	private bool isSuperTilting = false;


	void Awake() {
		rotatePlatform = (RotatingPlatform) gameObject.GetComponentInChildren<RotatingPlatform>();
		translation = (Translation) gameObject.GetComponentInChildren<Translation>();
	}
	void Start () {
		forwardSpeed = startSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//Calculate Forward Speed
        forwardSpeed = startSpeed + (speedIncrementPerLevel* StatManager.Instance.Speed.Level);

		//Calculate Side Speed
		if(Input.GetAxis("Horizontal") != 0) 
			ChangeSideSpeed(Input.GetAxis("Horizontal"));
		else
			StabilizeSideSpeed();

		//Check isTilting
		if(Input.GetAxis("Fire1") != 0 && isSuperTilting == false) {
			isSuperTilting = true;
			StartCoroutine (SuperTilt(new Vector3(0,0,0), new Vector3(0,0,180),tiltTimeAnimation));
		}

		//Move it
		if(isDead == false) {
			transform.position += new Vector3(sideSpeed,0,forwardSpeed); //Move the ship
			TiltShip(); //Tilt the ship
		}

	}

	void StabilizeSideSpeed() { //Stabilize the ship when no key is pressed
		int axis;
		if(sideSpeed > 0) axis = -1;
		else axis = 1;

		if(Mathf.Abs(sideSpeed) <= deadTiltZone) {
			sideSpeed = 0;
			curSideSpeedMultiplier = 0;
		} else {
		

			curSideSpeedMultiplier += Mathf.Sign(axis) * (stabilizingControlratio*control*Time.deltaTime);
			curSideSpeedMultiplier = Mathf.Clamp(curSideSpeedMultiplier,-1,1);

			sideSpeed = Mathf.Lerp(0, sideSpeedLimit, Mathf.Abs (curSideSpeedMultiplier));
			sideSpeed *= Mathf.Sign(curSideSpeedMultiplier);

		}

		//Debug.Log (curSideSpeedMultiplier);
	}

	void ChangeSideSpeed(float axis) { //Change side speed when a key is pressed
		//Debug.Log ("side Sped");
		curSideSpeedMultiplier += Mathf.Sign(axis) * (control*Time.deltaTime);
		curSideSpeedMultiplier = Mathf.Clamp(curSideSpeedMultiplier,-1,1);

		sideSpeed = Mathf.Lerp(0, sideSpeedLimit, Mathf.Abs (curSideSpeedMultiplier));
		sideSpeed *= Mathf.Sign(curSideSpeedMultiplier);

	}

	void TiltShip() {
		//Calculate New Tilt
		float speedMultiplier = Mathf.Abs(sideSpeed)/sideSpeedLimit;
		float newTilt = Mathf.Lerp (0,maxTiltAngle,speedMultiplier);
		newTilt *= Mathf.Sign(-sideSpeed);

		//Apply new Tilt
		modelAndCam.localRotation = Quaternion.Euler(new Vector3(0,0,newTilt));

	}


	public void Kill(){
		isDead = true;
		StartCoroutine(DeathAnimation());
		Destroy(model.gameObject);
	}

	public void Spawn(){
		isDead = false;
	}

	public IEnumerator DeathAnimation() {
		//move camera
		rotatePlatform.ManuallyRotate(-75,2f,1);
		translation.Activate();

		yield return new WaitForSeconds(0.05f);
		Time.timeScale = 0.05f;
		yield return new WaitForSeconds(1.5f*(Time.timeScale)); // 
		Time.timeScale = 1f;
		slowMoEnded = true;

		yield return new WaitForSeconds(5f);
		Application.LoadLevel(Application.loadedLevel);
	}

	private IEnumerator SuperTilt(Vector3 startingRotation, Vector3 endRotation, float time) {
		float step = 0f; //raw step
		float rate = 1f/time; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			smoothStep = Mathf.SmoothStep(0f, 1f, step); // finding smooth step
			foreach (GameObject g in objectsToSuperTilt) {
				g.transform.localRotation = Quaternion.Euler(Vector3.Lerp(startingRotation, endRotation, (smoothStep)));
				/*
				g.transform.localRotation = new Vector3 (Quaternion.Euler(Vector3.Lerp(startingRotation, endRotation, (smoothStep)))),
				                                         Quaternion.Euler(Vector3.Lerp(startingRotation, endRotation, (smoothStep)))),
				                                         Quaternion.Euler(Vector3.Lerp(startingRotation, endRotation, (smoothStep))))); //lerp position
				                                         */
			}
			lastStep = smoothStep; //get previous last step
			yield return null;
		}

	}

}
