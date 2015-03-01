using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroUIBehaviour : MonoBehaviour {
	public float countdownValue;

	private Text _countdownText;

	void Awake() {
		_countdownText = transform.Find ("Text").GetComponent<Text>();
	}

	void Update() {
		_countdownText.text = ((int)countdownValue).ToString ();
	}
}
