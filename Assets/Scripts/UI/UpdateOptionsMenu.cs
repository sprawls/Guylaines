using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UpdateOptionsMenu : MonoBehaviour {

	public Toggle toggle_tilt;

	// Use this for initialization
	void Start () {
		toggle_tilt.isOn = PersitentOptions.instance.isUsingTiltControls;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
