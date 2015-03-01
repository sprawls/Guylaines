using UnityEngine;
using System.Collections;

public class MoveCameraFromSpeed : MonoBehaviour {

	private ShipControl shipControl;
	public float startDistance;
	public float endDistance;
	public float speedForMax = 5f;
	
	private Vector3 cameraAngle;
	private float curSpeed;
	public float temporarySpeed = 0;

	// Use this for initialization
	void Awake() {
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		shipControl = playerObj.GetComponentInChildren<ShipControl>();
	}

	void Start () {
		cameraAngle = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(shipControl.isDead == false) {
			curSpeed = shipControl.forwardSpeed + temporarySpeed;
			float distanceMultiplier = Mathf.Min(speedForMax,curSpeed);
			distanceMultiplier = Mathf.Lerp(startDistance,endDistance,distanceMultiplier/speedForMax);
			transform.localPosition = cameraAngle*distanceMultiplier;
		}
	}

	public void AddIntensity(float amt, float time1, float time2) {
		StartCoroutine(LerpIntensity(amt,time1,time2));
	}
	public IEnumerator LerpIntensity(float amt, float time1, float time2) {
		for(float i =0; i<1; i+= Time.deltaTime/time1) {
			temporarySpeed = Mathf.Lerp (0,amt,i);
			yield return null;
		}
		for(float i =0; i<1; i+= Time.deltaTime/time2) {
			temporarySpeed = Mathf.Lerp (amt,0,i);
			yield return null;
		}
		temporarySpeed = 0;
	}

}
