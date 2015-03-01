using UnityEngine;
using System.Collections;

public class ShakeShakeShake : MonoBehaviour {

	//is Shaking constantly
	public bool noTimer = false;
	// How long the object should shake for.
	public float shakeTimeLeft = 0f;
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float timeDecreaseFactor = 1.0f;
	public float shakeDecreseFactor = 1.0f;

	Vector3 originalPos;


	void Awake() {
	}

	void OnEnable() {
		originalPos = transform.localPosition;
	}
	
	void Update() {
		ReduceTimeLeft();
		ReduceIntensity();

		if(shakeAmount > 0 && shakeTimeLeft >0) {
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
		}
	}

	private void ReduceIntensity() {
		if (shakeAmount > 0) {
			shakeAmount -= Time.deltaTime * shakeDecreseFactor;
		}
		else {
			shakeAmount = 0f;
		}
	}

	private void ReduceTimeLeft() {
		if (shakeTimeLeft > 0) {
			shakeTimeLeft -= Time.deltaTime * timeDecreaseFactor;
		}
		else {
			shakeTimeLeft = 0f;
			transform.localPosition = originalPos;
		}
	}

	public void SetShake(float shakeTime, float shakeIntensity) {
		shakeTimeLeft = shakeTime;
		shakeAmount = shakeIntensity;
	}

	public void AddShake(float shakeTime, float shakeIntensity) {
		shakeTimeLeft += shakeTime;
		shakeAmount += shakeIntensity;
	}
}
