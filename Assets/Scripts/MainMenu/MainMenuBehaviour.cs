using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuBehaviour : MonoBehaviour {

	public static MainMenuBehaviour Instance { get; private set; }

	public GameObject MainMenuObject;
	public GameObject OptionsMenuObject;

	void Awake() {
		Instance = this;
	}

	public void EnterGame() {
		Application.LoadLevel("Main");
	}

	public void OnPlay() {
		EnterGame ();
	}

	public void OnInstructions() {
		//Debug.Log ("Instructions");
	}

	public void OnQuit() {
		Application.Quit();
	}

	public void OnChange_Options(){
		OptionsMenuObject.SetActive (true);
		MainMenuObject.SetActive (false);
	}

	public void OnChange_Menu() {
		OptionsMenuObject.SetActive (false);
		MainMenuObject.SetActive (true);
	}

	public void Toggle_TiltControls(Toggle t) {
		PersitentOptions.instance.isUsingTiltControls = t.isOn;
	}
}
