using UnityEngine;
using System.Collections;

// TODO : CHANGE to local position like for Vertical door mouvement to get correct translation !

public class Translation : MonoBehaviour {
	
	public float xAxis = 0f;
	public float yAxis = 0f;
	public float zAxis = 0f;
	public bool destroyAtEnd = false;
	public bool loops = false;
	public bool isSmooth = false;
	public bool needPower = false;
	public bool isOpposite = false;
	public bool isDeactivated = true;
	
	public float distance = 10f;
	public float time = 0f;
	public float delay = 0f; //Delay before animation starts, good when multiples Translation next to eachother !

	private bool hasPlayedDelay = false;
	private bool goingBack = false;
	private Vector3 start;
	private Vector3 end;
	public bool isPlaying = false;
	// Use this for initialization
	void Start () {

		//GetPositions ();
		start = transform.localPosition;
		end = transform.localPosition + (new Vector3 (distance * xAxis, distance * yAxis, distance * zAxis)); //Applying it as well as the correct distance
	}
	
	// Update is called once per frame
	void Update () {

	}

	void GetPositions() { //Get Start and End positions of the translation
		start = transform.localPosition;
		//Here we need to take into account the rotation of all the parents of the door and apply the inverse to the direction of the Vector
		Transform tParent = transform.parent;
		Quaternion parentRotation = Quaternion.identity;
		while(tParent != null) { // Getting the parent's total Rotations
			parentRotation = parentRotation * tParent.transform.localRotation;
			tParent = tParent.parent;
		}
		parentRotation = Quaternion.Inverse (parentRotation); //Getting the Parent's rotation Inverse
		end = transform.localPosition + (parentRotation * new Vector3 (distance * xAxis, distance * yAxis, distance * zAxis)); //Applying it as well as the correct distance
	}
	
	public void Activate() {
		if(isPlaying) return;
		else StartCoroutine(PlayAnimation());
		isDeactivated = false;
	}
	public void Deactivate() {
		isDeactivated = true;
	}

	public void ResetTranslation() {
		goingBack = true;
		isPlaying = false;
		Activate ();
	}
	
	IEnumerator PlayAnimation () {
		isPlaying = true;
		if(hasPlayedDelay == false) {
			yield return new WaitForSeconds(delay);
			hasPlayedDelay = true;
		}

		do {
			if(isSmooth) {
				if(!goingBack) StartCoroutine(PlaySmoothAnimation(transform.localPosition, end));
				else StartCoroutine(PlaySmoothAnimation(transform.localPosition, start));
			} else {
				if(!goingBack) StartCoroutine(PlayAnimation(transform.localPosition, end));
				else StartCoroutine(PlayAnimation(transform.localPosition, start));
			}
			yield return (new WaitForSeconds(time));
			if(destroyAtEnd) Destroy (gameObject);
		} while(loops);
		isPlaying = false;
	}
	
	IEnumerator PlaySmoothAnimation(Vector3 startingPosition, Vector3 endingPosition) {
		goingBack = !goingBack;
		float step = 0f; //raw step
		float rate = 1f/time; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			smoothStep = Mathf.SmoothStep(0f, 1f, step); // finding smooth step
			transform.localPosition = new Vector3 (Mathf.Lerp(startingPosition.x, endingPosition.x, (smoothStep)),
			                                       Mathf.Lerp(startingPosition.y, endingPosition.y, (smoothStep)),
			                                       Mathf.Lerp(startingPosition.z, endingPosition.z, (smoothStep))); //lerp position
			lastStep = smoothStep; //get previous last step
			yield return null;
		}
		//complete rotation
		if(step > 1.0) transform.localPosition = new Vector3 (Mathf.Lerp(startingPosition.x, endingPosition.x, (1f )),
		                                                      Mathf.Lerp(startingPosition.y, endingPosition.y, (1f )),
		                                                      Mathf.Lerp(startingPosition.z, endingPosition.z, (1f )));
	}
	
	IEnumerator PlayAnimation(Vector3 startingPosition, Vector3 endingPosition) {
		goingBack = !goingBack;
		float step = 0f; //raw step
		float rate = 1f/time; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			smoothStep = step; // finding smooth step
			transform.localPosition = new Vector3 (Mathf.Lerp(startingPosition.x, endingPosition.x, (smoothStep)),
			                                       Mathf.Lerp(startingPosition.y, endingPosition.y, (smoothStep)),
			                                       Mathf.Lerp(startingPosition.z, endingPosition.z, (smoothStep))); //lerp position
			lastStep = smoothStep; //get previous last step
			yield return null;
		}
		//complete rotation
		if(step > 1.0) transform.localPosition = new Vector3 (Mathf.Lerp(startingPosition.x, endingPosition.x, (1f)),
		                                                      Mathf.Lerp(startingPosition.y, endingPosition.y, (1f)),
		                                                      Mathf.Lerp(startingPosition.z, endingPosition.z, (1f)));
	}
}
