using UnityEngine;
using System.Collections;

public class PersitentOptions : MonoBehaviour {

	//We make a static variable to our MusicManager instance
	public static PersitentOptions instance { get; private set; }

	//Variables
	public bool isUsingTiltControls = false;
	
	//When the object awakens, we assign the static variable
	void Awake() 
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
		}
	}

}
