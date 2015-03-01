using UnityEngine;
using System.Collections;

public class MainMenuBehaviour : MonoBehaviour {

	public static MainMenuBehaviour Instance { get; private set; }

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
}
