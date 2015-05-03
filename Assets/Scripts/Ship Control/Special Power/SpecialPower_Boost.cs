using UnityEngine;
using System.Collections;

public class SpecialPower_Boost : SpecialPower {

	public float activationTime = 1.5f;
	public float boostSpeed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Activate(ShipControl controls) {
		StartCoroutine (Activation (controls));
	}

	IEnumerator Activation(ShipControl controls) {
		for (float i = 0f; i < 1f; i+=Time.deltaTime/0.1f) {
			controls.additionalSpeed = Mathf.Lerp(0f,boostSpeed,i);
			yield return null;
		}
		for (float i = 0f; i < 1f; i+=Time.deltaTime/activationTime) {
			controls.additionalSpeed = Mathf.Lerp(boostSpeed,0f,i);
			yield return null;
		}
		controls.additionalSpeed = 0f;
		Destroy (gameObject);
	}
}
