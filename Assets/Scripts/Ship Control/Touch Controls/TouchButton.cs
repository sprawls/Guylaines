using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchButton : MonoBehaviour {

	public bool isMiddle = false;
	public bool isLeft = false;
	public bool isRight = false;
    public bool isShoot = false;

	private InputManager inputManager;


	//New input
	public List<int> inputIds = new List<int>(); //Ids of input on this buttoon right now

	void Start() {
		inputManager = GameObject.FindGameObjectWithTag ("InputManager").GetComponentInChildren<InputManager> ();
	}

	void Update() {

		if (inputIds.Count > 0) {
			if (isMiddle) {
				inputManager.AddMiddleButton (gameObject);
			}
			if (isLeft) {
				inputManager.AddLeftButton (gameObject);
			}
			if (isRight) {
				inputManager.AddRightButton (gameObject);	
			}
            if (isShoot) {
                inputManager.AddShootButton(gameObject);
            }
		} else {
			inputManager.RemoveMiddleButton(gameObject);
			inputManager.RemoveLeftButton(gameObject);
			inputManager.RemoveRightButton(gameObject);
		}
	}



	void FingerOn(int id) {
		if (!inputIds.Contains (id)) {
			inputIds.Add(id);
		}
	}

	void FingerOff(int id) {
		if (inputIds.Contains (id)) {
			inputIds.Remove(id);
		}
	}

	void DoubleClickOn(int id) {
		if (isLeft) {
			inputManager.AddDoubleClickLeft (gameObject);
		}
		if (isRight) {
			inputManager.AddDoubleClickRight (gameObject);	
		}
	}
	
}
