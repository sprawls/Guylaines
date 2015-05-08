using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public LayerMask TouchInputMask;
	private Camera InputCamera;
	private InputManager inputManager;

	//Swipe Struct
	private struct SwipeInfo {
		public bool canBeSwipe;
		public Vector2 StartPos;
		public float StartTime;
		public float idleTime;
	}

	//New Input
	private GameObject[] touchesTargets;
	private SwipeInfo[] SwipeInfos;
	//swipe values
	private float comfortZone = 100;
	private float maxSwipeTime = 1.5f;
	private float minSwipeDist = 100f;
	private float maxIdleTime = 0.2f;

	void Start() {
		inputManager = GameObject.FindGameObjectWithTag ("InputManager").GetComponentInChildren<InputManager> ();
		InputCamera = GetComponent<Camera> ();
		touchesTargets = new GameObject [6];
		SwipeInfos = new SwipeInfo[6];
	}

	void Update () {

		for (int t = 0; t<Input.touches.Length; ++t ) {
			processATouchPerFingerCodeNumber(Input.touches[t], Input.touches[t].fingerId );
		}


	}

	void processATouchPerFingerCodeNumber(Touch t, int n ) {
		if (n >= 6)
			return; //Only support up to five fingers

		if (t.phase == TouchPhase.Began) {
			AddInputAt (t, n);
			return;

		} else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) {
			RemoveInputAt (t, n);
			return;

		} else if (t.phase == TouchPhase.Moved) {
			MoveInputAt (t, n);
			return;

		} else {
			IdleInputAt(t,n);
			return;
		}

		// CHECK STATIONARY HERE IF NEEDED LATER //

	}

	void AddInputAt(Touch touch, int n) {
		Ray ray = InputCamera.ScreenPointToRay(touch.position);
		RaycastHit hit;
		//Swipe Check
		SwipeInfos [n].canBeSwipe = true;
		SwipeInfos [n].StartTime = Time.time;
		SwipeInfos [n].StartPos = touch.position;
		SwipeInfos [n].idleTime = 0;
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
		//check Swipe 
		float swipeTime = Time.time - SwipeInfos[n].StartTime;
		float swipeDist = (touch.position - SwipeInfos[n].StartPos).magnitude;

		//Debug.Log ("Swipe " + n + " : " + SwipeInfos[n].canBeSwipe + "   time : " + swipeTime + "     dist : " + swipeDist);

		if (SwipeInfos[n].canBeSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist)) {
			//We have a vertical swipe :
			inputManager.AddMiddleButton(gameObject);
		}

		//remove input
		RemoveInputFromButton (touch,n);

	}

	void  MoveInputAt(Touch touch, int n) {
		Ray ray = InputCamera.ScreenPointToRay(touch.position);
		RaycastHit hit;
		//Handle Swipe
		if (Mathf.Abs(touch.position.x - SwipeInfos[n].StartPos.x) > comfortZone) { //Cant be swipe is deviate too much from a straith line
			SwipeInfos[n].canBeSwipe = false;
		}
		SwipeInfos [n].idleTime = 0;

		//check Button presses
		if (Physics.Raycast (ray, out hit, TouchInputMask)) {

			GameObject newRecipient = hit.transform.gameObject;
			if(touchesTargets [n] != null && touchesTargets [n] == newRecipient) {
				return;//We are on the same target even though we moved, handle swipes here
			} else {
				RemoveInputFromButton(touch, n);
				newRecipient.SendMessage ("FingerOn", n, SendMessageOptions.DontRequireReceiver);
				touchesTargets[n] = newRecipient;
			}


		
		}
	}

	void IdleInputAt(Touch touch, int n) {
		SwipeInfos [n].idleTime += Time.deltaTime;
		//Debug.Log ("Swipe " + n + " idle for : " + SwipeInfos [n].idleTime );
		if (SwipeInfos [n].idleTime > maxIdleTime) {
			SwipeInfos [n].canBeSwipe = false; //Cant be swipe if idle
			//Debug.Log ("Swipe Cancelled by idle : " + n);
		}

	}

	void RemoveInputFromButton(Touch touch, int n){
		if (touchesTargets [n] != null) {
			GameObject recipient = touchesTargets [n];
			recipient.SendMessage ("FingerOff", n, SendMessageOptions.DontRequireReceiver);
		}
		touchesTargets [n] = null;
	}

}
