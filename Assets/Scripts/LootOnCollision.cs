﻿using UnityEngine;
using System.Collections;

public class LootOnCollision : MonoBehaviour {


    private ShipControl shipControl;
    private GameObject playerObj;
    private int itemStr;
    // Use this for initialization
    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        shipControl = playerObj.GetComponentInChildren<ShipControl>();
    }

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            itemStr = collision.gameObject.GetComponentInParent<Item>().basePower;
            Destroy(collision.gameObject.transform.parent.gameObject);
            StatManager.Instance.choisirItem(itemStr);
        }
        

    }

	// Update is called once per frame
	void Update () {
	
	}
}