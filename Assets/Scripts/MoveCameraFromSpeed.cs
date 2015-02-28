using UnityEngine;
using System.Collections;

public class MoveCameraFromSpeed : MonoBehaviour {

	public ShipControl shipControl;
	public float startDistance;
	public float endDistance;
	public float speedForMax = 5f;
	
	private Vector3 cameraAngle;
	private float curSpeed;

	// Use this for initialization
	void Start () {
		cameraAngle = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		curSpeed = shipControl.forwardSpeed;
		float distanceMultiplier = Mathf.Min(speedForMax,curSpeed);
		distanceMultiplier = Mathf.Lerp(startDistance,endDistance,distanceMultiplier/speedForMax);
		transform.localPosition = cameraAngle*distanceMultiplier;
	}
}
