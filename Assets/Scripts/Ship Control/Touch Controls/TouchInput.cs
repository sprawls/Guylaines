using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public LayerMask TouchInputMask;
	private Camera InputCamera;


	//New Input
	private GameObject[] touchesTargets;

	void Start() {
		InputCamera = GetComponent<Camera> ();
		touchesTargets = new GameObject [6];
	}

	void Update () {

		for (int t = 0; t<Input.touches.Length; ++t ) {
			processATouchPerFingerCodeNumber(Input.touches[t], Input.touches[t].fingerId );
		}


	}

	void processATouchPerFingerCodeNumber(Touch t, int n ) {

		if ( t.phase == TouchPhase.Began ) {
			
			if ( n >= 6) return;
			//Only support up to five fingers
			AddInputAt(t,n);
			return;
		}
		if ( t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled ) {

			RemoveInputAt(t,n);
			return;
		}
		if ( t.phase == TouchPhase.Moved ) {
			MoveInputAt(t,n);
			return;
		}

		// CHECK STATIONARY HERE IF NEEDED LATER //

	}

	void AddInputAt(Touch touch, int n) {
		Ray ray = InputCamera.ScreenPointToRay(touch.position);
		RaycastHit hit;

		//Debug.Log ("Finger id " + n + " has a Tapcount of " + touch.tapCount);
		if (Physics.Raycast (ray, out hit, TouchInputMask)) {
			
			GameObject recipient = hit.transform.gameObject;
			recipient.SendMessage ("FingerOn", n, SendMessageOptions.DontRequireReceiver);
			touchesTargets[n] = recipient;

			//Check for double click
			if(touch.tapCount > 1) {
				recipient.SendMessage ("DoubleClickOn", n, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void RemoveInputAt(Touch touch, int n) {

		if (touchesTargets [n] != null) {
			GameObject recipient = touchesTargets [n];
			recipient.SendMessage ("FingerOff", n, SendMessageOptions.DontRequireReceiver);
		}
		touchesTargets [n] = null;
	}

	void  MoveInputAt(Touch touch, int n) {
		Ray ray = InputCamera.ScreenPointToRay(touch.position);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, TouchInputMask)) {
			
			GameObject newRecipient = hit.transform.gameObject;

			if(touchesTargets [n] != null && touchesTargets [n] == newRecipient) {
				//We are on the same target even though we moved, handle swipes here
			} else {
				RemoveInputAt(touch, n);
				newRecipient.SendMessage ("FingerOn", n, SendMessageOptions.DontRequireReceiver);
				touchesTargets[n] = newRecipient;
			}


		
		}
	}
	

}
