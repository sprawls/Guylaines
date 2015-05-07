using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XpLink : MonoBehaviour {

	private class xpLinkObjects {
		public LineRenderer lineRenderer;
		public GameObject gameObject;
	}

	public GameObject xpLinkObject;
	private List<xpLinkObjects> currentXpLinks; //objects to create link with
	private Transform ship;

	// Use this for initialization
	void Start () {
		ship = GameObject.FindGameObjectWithTag ("Player").transform;
		currentXpLinks = new List<xpLinkObjects> ();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i< currentXpLinks.Count; i++) {
			currentXpLinks[i].lineRenderer.SetPosition(0,ship.position);
			currentXpLinks[i].lineRenderer.SetPosition(1,currentXpLinks[i].gameObject.transform.position);
		}
	}

	public void AddObjectToList(GameObject obj,Color obj_col) {
		if (obj != null) {
			xpLinkObjects x = new xpLinkObjects();
			x.lineRenderer = CreateXpLineRenderer(obj_col);
			x.gameObject = obj;
			currentXpLinks.Add (x);
		}
			
	}

	public void RemoveObjectToList(GameObject obj) {
		if (obj != null) {
			for(int i=0; i<currentXpLinks.Count; i++) {
				if(currentXpLinks[i].gameObject = obj) {
					DestroyXPLinkObject(currentXpLinks[i]);
					currentXpLinks.RemoveAt(i);
					break;
				}
			}
		}
	}

	void DestroyXPLinkObject(xpLinkObjects xpToDestroy) {
		Destroy (xpToDestroy.lineRenderer.gameObject);
	}

	LineRenderer CreateXpLineRenderer(Color c) {
		LineRenderer l = Instantiate (xpLinkObject).GetComponent<LineRenderer> ();
		l.SetColors (c, c);
		return (l);
	}
}
