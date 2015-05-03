using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XpLink : MonoBehaviour {

	private List<GameObject> collidingObjects; //objects to create link with


	// Use this for initialization
	void Start () {
		collidingObjects = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddObjectToList(GameObject obj) {
		if (obj != null) {
			collidingObjects.Add (obj);
		}
			
	}

	public void RemoveObjectToList(GameObject obj) {
		if (obj != null)
			collidingObjects.Remove (obj);
	}
}
