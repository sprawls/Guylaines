using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public LayerMask TouchInputMask;
	private Camera InputCamera;

	//Check release
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] oldTouches;

	void Start() {
		InputCamera = GetComponent<Camera> ();
	}

	void Update () {


		CheckTouchInputs();
		#if UNITY_EDITOR
		CheckTouchInputs_EDITOR();
		#endif

	}


	void CheckTouchInputs() {
		if(Input.touches.Length > 0) {
			
			//Copy the current touches in old
			oldTouches = new GameObject[touchList.Count];
			touchList.CopyTo(oldTouches);
			touchList.Clear();
			
			//Compute all current touches
			foreach (Touch touch in Input.touches) {
				
				Ray ray = InputCamera.ScreenPointToRay(touch.position);
				RaycastHit hit;
				
				if(Physics.Raycast(ray,out hit, TouchInputMask)) {
					
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);
					
					if(touch.phase == TouchPhase.Began) {
						recipient.SendMessage("OnTouchDown",hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Ended) {
						recipient.SendMessage("OnTouchUp",hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
						recipient.SendMessage("OnTouchStay",hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Canceled) {
						recipient.SendMessage("OnTouchExit",hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
				}
			}
			
			//Check for released touches
			foreach( GameObject go in oldTouches){
				if(!touchList.Contains(go)) {
					go.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	void CheckTouchInputs_EDITOR() {
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
			
			//Copy the current touches in old
			oldTouches = new GameObject[touchList.Count];
			touchList.CopyTo(oldTouches);
			touchList.Clear();
			
			Ray ray = InputCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit, TouchInputMask)) {
				
				GameObject recipient = hit.transform.gameObject;
				touchList.Add(recipient);
				
				if(Input.GetMouseButtonDown(0)) {
					recipient.SendMessage("OnTouchDown",hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if(Input.GetMouseButtonUp(0)) {
					recipient.SendMessage("OnTouchUp",hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if(Input.GetMouseButton(0)) {
					recipient.SendMessage("OnTouchStay",hit.point, SendMessageOptions.DontRequireReceiver);
				}
				
			}
	
			
			//Check for released touches
			foreach( GameObject go in oldTouches){
				if(!touchList.Contains(go)) {
					go.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

}
