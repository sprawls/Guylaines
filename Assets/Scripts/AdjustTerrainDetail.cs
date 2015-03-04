using UnityEngine;
using System.Collections;

public class AdjustTerrainDetail : MonoBehaviour {

	public float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Terrain.activeTerrain.detailObjectDistance = distance;
	}
}
