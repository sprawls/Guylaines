using UnityEngine;
using System.Collections;

public class XpOnCollision : MonoBehaviour {


    public GameObject GlowParticul;
    private ShipControl shipControl;
    private GameObject playerObj;
    // Use this for initialization
    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        shipControl = playerObj.GetComponentInChildren<ShipControl>();
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            XpInformation XpInfo=collision.gameObject.GetComponent<XpInformation>();
            int XpType = XpInfo.XpType;
            if (XpType > 0)
            {
                float dist=Vector3.Distance(collision.gameObject.transform.position, playerObj.transform.position);
                string xpText="";
                float xpStrenght=30-dist;
                switch(XpType)
                {
                    case 1:
                        
                        xpText+="Speed xp: "+dist;
                        break;
                    case 2:
                        xpText+="Handle xp: "+dist;
                        break;
                    case 3:
                        xpText+="Energie xp: "+dist;
                        break;

                }
                XpInfo.glow(dist);

                Debug.Log("Collided with : " + collision.gameObject + " - " + xpText);
            }

        }

    }
}
