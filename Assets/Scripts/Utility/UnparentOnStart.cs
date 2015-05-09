using UnityEngine;
using System.Collections;

public class UnparentOnStart : MonoBehaviour {


	// Use this for initialization
	void Start () {
        transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
