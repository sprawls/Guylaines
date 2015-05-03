using UnityEngine;
using System.Collections;

public class RotateCameraOnObjectRotation : MonoBehaviour {

	public GameObject objToRotateTo;
	public float step = 0.5f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion targetRot = Quaternion.FromToRotation (transform.localEulerAngles, objToRotateTo.transform.localEulerAngles);
		//transform.localRotation = Quaternion.Lerp( Quaternion.identity, targetRot, step);
		transform.localRotation = Quaternion.Slerp (transform.localRotation, objToRotateTo.transform.localRotation, step);
	}
}
