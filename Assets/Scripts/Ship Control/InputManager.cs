using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	private ShipControl shipControl;

	public List<GameObject> LeftButtonsActive;
	public List<GameObject> RightButtonsActive;
	public List<GameObject> MiddleButtonsActive;



	void Start() {
		shipControl = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ShipControl> ();

		LeftButtonsActive = new List<GameObject> ();
		RightButtonsActive = new List<GameObject> ();
		MiddleButtonsActive = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckKeyboardControls (); //This could be put in another script

		if(LeftButtonsActive.Count > 0) shipControl.Input_Left = true;
		else shipControl.Input_Left = false;

		if(RightButtonsActive.Count > 0) shipControl.Input_Right = true;
		else shipControl.Input_Right = false;

		if(MiddleButtonsActive.Count > 0) shipControl.Input_Special = true;
		else shipControl.Input_Special = false;
	}

	// ADD BUTTONS METHODS //
	public void AddLeftButton(GameObject g) {
		if(!LeftButtonsActive.Contains(g)) LeftButtonsActive.Add (g);
	}
	public void RemoveLeftButton(GameObject g) {
		if(LeftButtonsActive.Contains(g)) LeftButtonsActive.Remove(g);
	}

	public void AddRightButton(GameObject g) {
		if(!RightButtonsActive.Contains(g)) RightButtonsActive.Add (g);
	}
	public void RemoveRightButton(GameObject g) {
		if(RightButtonsActive.Contains(g)) RightButtonsActive.Remove(g);
	}

	public void AddMiddleButton(GameObject g) {
		if(!MiddleButtonsActive.Contains(g)) MiddleButtonsActive.Add (g);
	}
	public void RemoveMiddleButton(GameObject g) {
		if(MiddleButtonsActive.Contains(g)) MiddleButtonsActive.Remove(g);
	}




	void CheckKeyboardControls (){
		//Mouvement
		if (Input.GetAxis ("Horizontal") < 0) {
			AddLeftButton(gameObject);
			RemoveRightButton(gameObject);
		} else if(Input.GetAxis("Horizontal") > 0) {
			AddRightButton(gameObject);
			RemoveLeftButton(gameObject);
		} else {
			RemoveRightButton(gameObject);
			RemoveLeftButton(gameObject);
		}
	}
}
