
using UnityEngine;
using System.Collections;

public class Player_BossTurret : MonoBehaviour {

	//Components Model
	[Header("Animation Components")]
	public GameObject CannonModel;
	public GameObject Model;

	//components Particles
	[Header("Particles Components")]
	public ParticleSystem chargeParticles;
	public ParticleSystem readyParticles;
    public GameObject shootParticles;
    public GameObject hitParticles;

	[Header("Stats")]

	//Stats
	public float reloadTime = 5f;
    private LayerMask bossLayerMask = (1 << 12); //Only Boss Layer

	//States
	[HideInInspector] public bool canChangeActivation = true;
	private bool shoot_ready;


	//animations
	private float animationTime_Activation = 1.5f;



	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.X) && shoot_ready) {
			Shoot ();
		}
	}

	public void Activate() {
		StartCoroutine (ActivateCoroutine ());
		StartCoroutine(ModelAnimation(new Vector3(0,-0.5f,0), new Vector3(0,0,0)));
		StartCoroutine (CannonAnimation (90, 25));
		StartCoroutine (Reload ());
	}

	public void Deactivate() {
        StopAllCoroutines();
		StartCoroutine (CannonAnimation (25, 90));
		StartCoroutine(ModelAnimation(new Vector3(0,0,0), new Vector3(0,-0.5f,0)));
		StartCoroutine (DeactivateCoroutine ());

		chargeParticles.Stop ();
		readyParticles.Stop ();
		shoot_ready = false;
	}

	void Shoot() {
        //Logic
        //TODO ONCE BOSS IS PARTLY DONE
        Debug.DrawRay(chargeParticles.transform.position, new Vector3(0, 80, 500), Color.white, 5f);

        RaycastHit hit;
        if (Physics.Raycast(chargeParticles.transform.position, new Vector3(0, 80, 500).normalized,out hit, new Vector3(0, 80, 500).magnitude, bossLayerMask)) {
            Debug.Log(hit.collider.gameObject);
            Instantiate(hitParticles, hit.point, Quaternion.identity);
        }

        //Visuals
        Vector3 target = new Vector3(0, 100, 500);
        ShowShootParticles(target);
		

        //Start Reload
		StartCoroutine (Reload ());
	}

    void ShowShootParticles(Vector3 targetPosition) {
        GameObject newParticles = ((GameObject)Instantiate(shootParticles, chargeParticles.transform.position, Quaternion.identity));
        SetBeamValues beamValues = newParticles.GetComponentInChildren<SetBeamValues>();
        beamValues.SetBeamPositions(Vector3.zero, targetPosition);
        readyParticles.Emit(100);
    }


	IEnumerator CannonAnimation(float s, float e) {
		canChangeActivation = false;
		for (float i = 0; i < 1; i += Time.deltaTime/animationTime_Activation) {
			CannonModel.transform.localEulerAngles = new Vector3(Mathf.Lerp (s,e,i), 0,0 );
			yield return null;
		}
		CannonModel.transform.localEulerAngles = new Vector3(e,0,0);
		canChangeActivation = true;

	}

	IEnumerator ModelAnimation(Vector3 s, Vector3 e) {
		for (float i = 0; i < 1; i += Time.deltaTime/animationTime_Activation) {
			Model.transform.localPosition = Vector3.Lerp (s,e,i);
			yield return null;
		}
		Model.transform.localPosition = e;
	}

	IEnumerator ActivateCoroutine() {
		Model.SetActive (true);
		yield return new WaitForSeconds (animationTime_Activation);

	}

	IEnumerator DeactivateCoroutine() {
		yield return new WaitForSeconds (animationTime_Activation);
		Model.SetActive (false);
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
