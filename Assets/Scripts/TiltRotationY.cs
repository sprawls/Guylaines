using UnityEngine;
using System.Collections;

public class TiltRotationY : MonoBehaviour {


	public bool rotateX = false;
	public bool rotateY = false;
	public bool rotateZ = false;
	public float maxRotation = 10f;
	public bool isInverse = false;

	private ShipControl shipControl;
	private Vector3 BaseRotation;
	private float curSpeed;
	private float maxSpeed;

	// Use this for initialization
	void Awake() {
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		shipControl = playerObj.GetComponentInChildren<ShipControl>();
	}

	void Start () {
		BaseRotation = transform.localRotation.eulerAngles;
		maxSpeed = shipControl.sideSpeedLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if(rotateX) AdjustAngleX();
		if(rotateY) AdjustAngleY();
		if(rotateZ) AdjustAngleZ();
	}

	void AdjustAngleX() {
		curSpeed = shipControl.sideSpeed;
		float distanceMultiplier = Mathf.Min(maxSpeed,Mathf.Abs(curSpeed));
		distanceMultiplier = Mathf.Lerp(0,maxRotation,distanceMultiplier/maxSpeed);
		distanceMultiplier *= Mathf.Sign(curSpeed);
		if(isInverse) distanceMultiplier *= -1;

		//Debug.Log (distanceMultiplier);
		transform.localRotation = Quaternion.Euler(BaseRotation + new Vector3(distanceMultiplier,0,0));
	}

	void AdjustAngleY() {
		curSpeed = shipControl.sideSpeed;
		float distanceMultiplier = Mathf.Min(maxSpeed,Mathf.Abs(curSpeed));
		distanceMultiplier = Mathf.Lerp(0,maxRotation,distanceMultiplier/maxSpeed);
		distanceMultiplier *= Mathf.Sign(curSpeed);
		if(isInverse) distanceMultiplier *= -1;

		//Debug.Log (distanceMultiplier);
		transform.localRotation = Quaternion.Euler(BaseRotation + new Vector3(0,distanceMultiplier,0));
	}

	void AdjustAngleZ() {
		curSpeed = shipControl.sideSpeed;
		float distanceMultiplier = Mathf.Min(maxSpeed,Mathf.Abs(curSpeed));
		distanceMultiplier = Mathf.Lerp(0,maxRotation,distanceMultiplier/maxSpeed);
		distanceMultiplier *= Mathf.Sign(curSpeed);
		if(isInverse) distanceMultiplier *= -1;

		//Debug.Log (distanceMultiplier);
		transform.localRotation = Quaternion.Euler(BaseRotation + new Vector3(0,0,distanceMultiplier));
	}


}
