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
		Vector3 hitPoint = new Vector3(0,0,0);
		Vector3 surfaceNormal = new Vector3(0,0,0);
	    Vector3 fwd = transform.forward;

		bool hashit = false;

        if (Physics.Raycast(groundingRay, out hit, 25f, floorLayerMask))
        {
			targetHeight = hit.point.y + additionalHeight;
			surfaceNormal = hit.normal;
			hitPoint = hit.point;
			hashit = true;
		}
		//Debug.DrawRay(groundingRay.origin,groundingRay.direction,Color.yellow);

		//Change Orientation of ship
		if(hashit) {

			Vector3 proj = fwd - (Vector3.Dot(fwd, hit.normal)) * hit.normal;
			Quaternion targetRot = Quaternion.LookRotation(proj, hit.normal);
		
			float time = Time.deltaTime * 1f;
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, time);

			//Change Height of ship
			/* method 1
			Vector3 targetPosition = new Vector3(transform.position.x,
			                             targetHeight,
			                             transform.position.z);
			transform.position = Vector3.Lerp (transform.position,targetPosition,0.5f);
			*/
			Vector3 targetPosition = hit.point + (transform.up*additionalHeight);
			transform.position = targetPosition;
		}
		////Debug.Log ("transform rotation" +transform.rotation + "          rotation : " + surfaceNormal);
	}
}
