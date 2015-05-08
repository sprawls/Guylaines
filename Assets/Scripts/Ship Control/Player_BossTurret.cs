using UnityEngine;
using System.Collections;

public class Player_BossTurret : MonoBehaviour {

	//Components
	public GameObject CannonModel;
	public ParticleSystem chargeParticles;
	public ParticleSystem readyParticles;

	public bool canChangeActivation = true;

	//Stats
	public float reloadTime = 5f;
	private bool shoot_ready;


	//animations
	private float animationTime_Activation = 1.5f;



	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.X) && shoot_ready) {
			Debug.Log ("Shoot");
			Shoot ();
		}
	}

	public void Activate() {
		StartCoroutine (CannonAnimation (25, 90));
		StartCoroutine (Reload ());
	}

	public void Deactivate() {
		StartCoroutine (CannonAnimation (90, 25));


		chargeParticles.Stop ();
		readyParticles.Stop ();
		shoot_ready = false;
	}

	void Shoot() {
		readyParticles.Emit (100);
		StartCoroutine (Reload ());
	}
	

	IEnumerator CannonAnimation(float s, float e) {
		canChangeActivation = false;
		for (float i = 0; i < 1; i += Time.deltaTime/animationTime_Activation) {
			CannonModel.transform.localEulerAngles = new Vector3(Mathf.Lerp (e,s,i), 0,0 );
			yield return null;
		}
		canChangeActivation = true;
	}

	IEnumerator Reload() {
		shoot_ready = false;
		chargeParticles.Play ();
		readyParticles.Stop ();
		yield return new WaitForSeconds (reloadTime);
		chargeParticles.Stop ();
		readyParticles.Play ();
		shoot_ready = true;
	}
}
