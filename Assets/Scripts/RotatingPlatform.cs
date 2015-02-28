using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour {

	/*  Put this script on an empty GameObject that serves as the axis of Rotation for its parent.
	 *  The Object will automatically turn every "interval" seconds. 
	 * 	To do it manually, set to 0 and call function ManuallyRotate with wanted parameters.
	 */

	public float interval = 4f;
	public float rotationTime = 2f;
	public float rotationAngle = 90f;
	public int rotationDirection = 1; // set to -1 for opposite direction
	public bool isSmooth = true;
	public bool rotateX = false;
	public bool rotateY = true;
	public bool rotateZ = false;
	public bool randomize = false;
	public float randomizeInterval = 2f;

	private float maxRotationAngle = 500f;
	private float minRotationAngle = 150f;


	void Start () {
		StartCoroutine (RotateTimer ());
		if (randomize == true) StartCoroutine(RandomizeRotation());
	}
	
	// Update is called once per frame
	public void ManuallyRotate(float angle,float time,int direction) {
		StartCoroutine (RotatePlatform(transform,angle,time,direction));
	}

	IEnumerator RotateTimer() {
		while (true) {
			yield return(new WaitForSeconds(interval));
			StartCoroutine (RotatePlatform(transform,rotationAngle,rotationTime,rotationDirection));
		}
	}

	IEnumerator RotatePlatform(Transform point, float rotateAmount, float rotateTime, int rotateDirection) {
		float step = 0f; //raw step
		float rate = 1f/rotateTime; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			if(isSmooth) {
				smoothStep = Mathf.SmoothStep(0f, 1f, step); // finding smooth step
			} else {
				smoothStep = step;
			}
			if(rotateX == true) transform.parent.RotateAround(point.position, point.forward, (rotateDirection*rotateAmount) * (smoothStep - lastStep));
			if(rotateY == true) transform.parent.RotateAround(point.position, point.up, (rotateDirection*rotateAmount) * (smoothStep - lastStep));
			if(rotateZ == true) transform.parent.RotateAround(point.position, point.right, (rotateDirection*rotateAmount) * (smoothStep - lastStep));
			lastStep = smoothStep; //get previous last step
			yield return null;
		}
		//complete rotation
		if(step > 1.0 && rotateX) transform.RotateAround(point.position, point.forward, rotateAmount * (1f - lastStep));
		if(step > 1.0 && rotateY) transform.RotateAround(point.position, point.up, rotateAmount * (1f - lastStep));
		if(step > 1.0 && rotateZ) transform.RotateAround(point.position, point.right, rotateAmount * (1f - lastStep));
	}

	IEnumerator RandomizeRotation() {
		while(true) {
			yield return (new WaitForSeconds(randomizeInterval));
			int rand = Random.Range (1,100);

			if(rand > 50) StartCoroutine(ChangeSpeed(rand/4f));
			else StartCoroutine(ChangeSpeed(-rand/4f));

			if(rand % 7 == 0) rotateX = !rotateX;
			if(rand % 8 == 0) rotateY = !rotateY;
			if(rand % 9 == 0) rotateZ = !rotateZ;
			if(rotateX == false && rotateY == false && rotateZ == false) rotateZ = true;
		}
	}

	IEnumerator ChangeSpeed(float rotChange) {
		float startingPosition = rotationAngle;
		float endingPosition = rotationAngle + rotChange;
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime*(1f/randomizeInterval)) { //for the length of the randomize interval
			rotationAngle = Mathf.Lerp (startingPosition, endingPosition, i); // We lerp the rotation 
			yield return null;
		}
		if(rotationAngle > maxRotationAngle) rotationAngle = maxRotationAngle;
		else if (rotationAngle < minRotationAngle) rotationAngle = minRotationAngle;
	}
}
