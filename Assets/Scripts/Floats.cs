using UnityEngine;
using System.Collections;

public class Floats : MonoBehaviour {

	public float floatTime = 3f;
	public float amplitude = 1f;
	public float delay = 0f; //Delay before it starts. Allow better feel when multiple floating object are near eachother.

	public float timefor90deg = 2f;


	private float maxY;
	private float minY;

	public bool rotates = true;

	void Start () {
		maxY = transform.localPosition.y + amplitude;
		minY = transform.localPosition.y - amplitude;
		StartCoroutine(PlayAnimation());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlayAnimation () {
		yield return (new WaitForSeconds (delay));
		StartCoroutine (rotateAnimation ());
		while(true) {
			StartCoroutine(PlaySmoothAnimation(minY, maxY));
			yield return (new WaitForSeconds(floatTime));
			StartCoroutine(PlaySmoothAnimation(maxY, minY));
			yield return (new WaitForSeconds(floatTime));
		}
	}

	IEnumerator PlaySmoothAnimation(float startingPosition, float endingPosition) {
		float step = 0f; //raw step
		float rate = 1f/floatTime; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			smoothStep = Mathf.SmoothStep(0f, 1f, step); // finding smooth step

			transform.localPosition = new Vector3 (transform.localPosition.x,Mathf.Lerp(startingPosition, endingPosition, (smoothStep)),transform.localPosition.z); //lerp position
			lastStep = smoothStep; //get previous last step
			yield return null;
		}
		//complete rotation
		//if(step > 1.0) transform.localPosition = new Vector3 (transform.localPosition.x,Mathf.Lerp(startingPosition, endingPosition, (1f - lastStep)),transform.localPosition.z);
		
	}

	IEnumerator rotateAnimation() {
		if (rotates == false)
						yield break;

		float startingPosition;
		float endingPosition;
		while(true) {
			startingPosition = transform.rotation.eulerAngles.y;
			endingPosition = startingPosition + 90f;
			for (float i = 0.0f; i < 1.0f; i += Time.deltaTime*(1f/timefor90deg)) { //for the length of the randomize interval
				transform.rotation = Quaternion.Euler(new Vector3( transform.rotation.eulerAngles.x, Mathf.Lerp (startingPosition, endingPosition, i), transform.rotation.eulerAngles.z)); // We lerp the rotation 
				yield return null;
			}
		}
	}
}
