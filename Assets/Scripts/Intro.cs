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
		GameObject boostObj = (GameObject) Instantiate (BoostParticles, Vector3.zero, Quaternion.identity);
		ShipPrefab.SetActive(true);

		boostObj.transform.parent = ShipPrefab.GetComponentInChildren<ShipControl>().transform;
		//Give camera a boost backward
		moveCam = ShipPrefab.GetComponentInChildren<MoveCameraFromSpeed>();
		moveCam.AddIntensity(3,0.05f,0.16f);


		Destroy (gameObject);
	}

	IEnumerator PlayIntro() {
		yield return new WaitForSeconds(2);

	}
}
