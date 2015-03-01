using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	public GameObject ShipPrefab;
	public GameObject BoostParticles;
	public MoveCameraFromSpeed moveCam;

	// Use this for initialization
	void Awake() {
		gameObject.tag = "intro";
		StartCoroutine (PlayIntro());
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame(){
		//Instantiate boost
		GameObject boostObj = (GameObject) Instantiate (BoostParticles, Vector3.zero, Quaternion.identity);
		//Activate Prefab and set boost postiion
		ShipPrefab.SetActive(true);
		boostObj.transform.parent = ShipPrefab.GetComponentInChildren<ShipControl>().transform;
		//Give camera a boost backward
		moveCam = ShipPrefab.GetComponentInChildren<MoveCameraFromSpeed>();
		moveCam.AddIntensity(3,0.05f,0.16f);
		//Barrel roll to awesomeness
		ShipControl shipControl = ShipPrefab.GetComponentInChildren<ShipControl>();
		shipControl.BarrelRoll(360,1.0f);

		Destroy (gameObject);
	}

	IEnumerator PlayIntro() {
		yield return new WaitForSeconds(2);

	}
}
