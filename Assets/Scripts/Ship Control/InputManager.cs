using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	private ShipControl shipControl;

	public List<GameObject> LeftButtonsActive;
	public List<GameObject> RightButtonsActive;
	public List<GameObject> MiddleButtonsActive;
	public bool DoubleClickedLeft = false;
	public bool DoubleClickedRight = false;
    public bool ShootButtonClicked = false;


	void Start() {
		shipControl = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ShipControl> ();

		LeftButtonsActive = new List<GameObject> ();
		RightButtonsActive = new List<GameObject> ();
		MiddleButtonsActive = new List<GameObject> ();
		DoubleClickedLeft = false;
		DoubleClickedRight = false;
        ShootButtonClicked = false;

	}
	
	// Update is called once per frame
	void Update () {

		CheckKeyboardControls (); //This could be put in another script

        //Mouvement Buttons
		if(LeftButtonsActive.Count > 0) shipControl.Input_Left = true;
		else shipControl.Input_Left = false;

		if(RightButtonsActive.Count > 0) shipControl.Input_Right = true;
		else shipControl.Input_Right = false;

		if (MiddleButtonsActive.Count > 0) {
			shipControl.Input_Special = true;
			MiddleButtonsActive.Clear();
		}
		else shipControl.Input_Special = false;

        //DoubleClicks
		if (DoubleClickedLeft == true) {
			DoubleClickedLeft = false;
			shipControl.Input_LeftRoll_OneTime = true;
		} else 
			shipControl.Input_LeftRoll_OneTime = false;

		if (DoubleClickedRight == true) {
			DoubleClickedRight = false;
			shipControl.Input_RightRoll_OneTime = true;
		} else
			shipControl.Input_RightRoll_OneTime = false;

        //Shoot Button
        if (ShootButtonClicked == true) {
            ShootButtonClicked = false;
            shipControl.Input_Shoot = true;
        } else {
            shipControl.Input_Shoot = false;
        }
	}


	// ADD BUTTONS METHODS //
	//Movement Left
	public void AddLeftButton(GameObject g) {
		if(!LeftButtonsActive.Contains(g)) LeftButtonsActive.Add (g);
	}
	public void RemoveLeftButton(GameObject g) {
		if(LeftButtonsActive.Contains(g)) LeftButtonsActive.Remove(g);
	}
	//Mouvement Right
	public void AddRightButton(GameObject g) {
		if(!RightButtonsActive.Contains(g)) RightButtonsActive.Add (g);
	}
	public void RemoveRightButton(GameObject g) {
		if(RightButtonsActive.Contains(g)) RightButtonsActive.Remove(g);
	}
	//Special
	public void AddMiddleButton(GameObject g) {
		if(!MiddleButtonsActive.Contains(g)) MiddleButtonsActive.Add (g);
	}
	public void RemoveMiddleButton(GameObject g) {
		if(MiddleButtonsActive.Contains(g)) MiddleButtonsActive.Remove(g);
	}
	//barrel Roll
	public void AddDoubleClickLeft(GameObject g) {
		DoubleClickedLeft = true;
	}
	public void AddDoubleClickRight(GameObject g) {
		DoubleClickedRight = true;
	}
    //Shoot Button
    public void AddShootButton(GameObject g) {
        ShootButtonClicked = true;
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
		//Special
		if (Input.GetAxis ("Fire1") != 0) {
			AddMiddleButton(gameObject);
		} else {
			RemoveMiddleButton(gameObject);
		}
		//Barrel Rolls
		if (Input.GetAxis("BarrelRollLeft") < 0 || Input.GetButtonDown("BarrelRollLeft")) {
			AddDoubleClickLeft(gameObject);
		}
		if (Input.GetAxis("BarrelRollRight") > 0 || Input.GetButtonDown("BarrelRollRight")) {
			AddDoubleClickRight(gameObject);
		}
        //Shoot
        if (Input.GetKeyDown(KeyCode.X)) {
            AddShootButton(gameObject);
        }
	}
}
