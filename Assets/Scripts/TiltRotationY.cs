using UnityEngine;
using System.Collections;

public class TiltRotationY : MonoBehaviour {

	public ShipControl shipControl;
	public float maxRotation = 10f;

	private Vector3 BaseRotation;
	private float curSpeed;
	private float maxSpeed;

	// Use this for initialization
	void Start () {
		BaseRotation = transform.localRotation.eulerAngles;
		maxSpeed = shipControl.sideSpeedLimit;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustAngle();
	}

	void AdjustAngle() {
		curSpeed = shipControl.sideSpeed;
		float distanceMultiplier = Mathf.Min(maxSpeed,Mathf.Abs(curSpeed));
		distanceMultiplier = Mathf.Lerp(0,maxRotation,distanceMultiplier/maxSpeed);
		distanceMultiplier *= Mathf.Sign(curSpeed);

		Debug.Log (distanceMultiplier);
		transform.localRotation = Quaternion.Euler(BaseRotation + new Vector3(0,distanceMultiplier,0));
	}
}
