using UnityEngine;
using System.Collections;

public class CheckHeight : MonoBehaviour {

	private float targetHeight;
	private int floorLayerMask = 1 << 10;

	public float additionalHeight = 2.75f;
	// Use this for initialization
	void Start () {
		targetHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//Raycast
		Ray groundingRay = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		Vector3 surfaceNormal = new Vector3(0,0,0);

		if(Physics.Raycast(groundingRay,out hit,100f,floorLayerMask)) {
			targetHeight = hit.point.y + additionalHeight;
			surfaceNormal = hit.normal;
		}
		//Change Height of ship
		Vector3 targetPosition = new Vector3(transform.position.x,
		                             targetHeight,
		                             transform.position.z);
		transform.position = Vector3.Lerp (transform.position,targetPosition,0.5f);
		//Change Orientation of ship
		//transform.eulerAngles = rotation;
		transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNormal);


		float headingDeltaAngle = Time.deltaTime * 0.01f;
		Quaternion headingDelta = Quaternion.AngleAxis(headingDeltaAngle, transform.up);
		//align with surface normal
		transform.rotation = Quaternion.FromToRotation( transform.up, surfaceNormal) * transform.rotation;
		//apply heading rotation
		transform.rotation = headingDelta * transform.rotation;

		Debug.Log ("transform rotation" +transform.rotation + "          rotation : " + surfaceNormal);
	}
}
