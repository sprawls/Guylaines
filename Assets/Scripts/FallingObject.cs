using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FallingObject : MonoBehaviour {

    public float fallDistance;

    private Transform player;
    private bool fallen = false;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Ship Prefab").transform;
        init();
	}

    public virtual void init()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		if(player == null) player = GameObject.Find("Ship Prefab").transform;

        Vector3 p1 = player.position;
        Vector3 p2 = transform.position;
        p1.y = 0;
        p2.y = 0;
        float distance = Vector3.Distance(p1,p2);
        
	    if(!fallen && distance < fallDistance)
        {
            fallen = true;
            DoSomethingWhenNear();
        }
	}

    public virtual void DoSomethingWhenNear()
    {
        TerrainGenerator tg = FindObjectOfType<TerrainGenerator>();
        RotatingPlatform[] rps = gameObject.GetComponentsInChildren<RotatingPlatform>();
        RotatingPlatform rp = rps.ToList().OneAtRandom(tg.rand);
        rp.ManuallyRotate();
    }
}
