using UnityEngine;
using System.Collections;

public class soundDelay : MonoBehaviour {

	public float delay = 0.2f;
	private AudioSource audioSource;
	// Use this for initialization

	void Start () {
		audioSource = (AudioSource) gameObject.GetComponent<AudioSource>();
		StartCoroutine(delaySound());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator delaySound(){
		yield return new WaitForSeconds(delay);
		audioSource.Play();
	}
}
