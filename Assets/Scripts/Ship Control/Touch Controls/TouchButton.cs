using UnityEngine;
using System.Collections;

public class TouchButton : MonoBehaviour {

	public bool isMiddle = false;
	public bool isLeft = false;
	public bool isRight = false;

	private InputManager inputManager;
	private int amountOfPresses = 0;

	void Start() {
		inputManager = GameObject.FindGameObjectWithTag ("InputManager").GetComponentInChildren<InputManager> ();
	}

	void Update() {
		if (amountOfPresses > 0) {
			if (isMiddle) {
				inputManager.AddMiddleButton (gameObject);
			}
			if (isLeft) {
				inputManager.AddLeftButton (gameObject);
			}
			if (isRight) {
				inputManager.AddRightButton (gameObject);	
			}
		} else {
			inputManager.RemoveMiddleButton(gameObject);
			inputManager.RemoveLeftButton(gameObject);
			inputManager.RemoveRightButton(gameObject);
		}
	}

	void OnTouchDown() {
		amountOfPresses ++;
	}

	void OnTouchUp() {
		amountOfPresses --;
	}

	void OnTouchStay() {

	}

	void OnTouchExit() {

	}
	
}
