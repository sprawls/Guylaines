using UnityEngine;
using System.Collections;

public class MoveCameraFromSpeed : MonoBehaviour {

	private ShipControl shipControl;
	public float startDistance;
	public float endDistance;
	public float speedForMax = 5f;
	
	private Vector3 cameraAngle;
	private float curSpeed;

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
			curSpeed = shipControl.forwardSpeed;
			float distanceMultiplier = Mathf.Min(speedForMax,curSpeed);
			distanceMultiplier = Mathf.Lerp(startDistance,endDistance,distanceMultiplier/speedForMax);
			transform.localPosition = cameraAngle*distanceMultiplier;
		}
	}
}
