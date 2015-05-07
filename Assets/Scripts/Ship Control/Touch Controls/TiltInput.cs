using UnityEngine;
using System.Collections;

public class TiltInput : MonoBehaviour {

	private InputManager inputManager;
	

	void Start () {
		inputManager = GameObject.FindGameObjectWithTag ("InputManager").GetComponentInChildren<InputManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PersitentOptions.instance.isUsingTiltControls) {
			if(Input.acceleration.x > 0.15f) {
				inputManager.AddRightButton (gameObject);	
				inputManager.RemoveLeftButton(gameObject);
			} else if(Input.acceleration.x < -0.15f) {
				inputManager.AddLeftButton (gameObject);
				inputManager.RemoveRightButton(gameObject);
			} else {
				inputManager.RemoveLeftButton(gameObject);
				inputManager.RemoveRightButton(gameObject);
			}
		}
	}
}
