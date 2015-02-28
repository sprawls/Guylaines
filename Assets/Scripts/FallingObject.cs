using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FallingObject : MonoBehaviour {

    public float fallDistance;

    private Transform player;
    private bool fallen = false;
    private RotatingPlatform rp;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Ship Prefab").transform;
        TerrainGenerator tg = FindObjectOfType<TerrainGenerator>();

        RotatingPlatform[] rps = gameObject.GetComponentsInChildren<RotatingPlatform>();
        rp = rps.ToList().OneAtRandom(tg.rand);
	}
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(player.position, transform.position);

        /*
        Debug.Log("Start");
        Debug.Log(player.localPosition);
        Debug.Log(transform.localPosition);
        Debug.Log(distance);
        Debug.Log(fallDistance);
         //*/
        
	    if(!fallen && rp != null && distance < fallDistance)
        {
            Debug.Log("Tournation");
            fallen = true;
            rp.ManuallyRotate();
        }
	}
}
