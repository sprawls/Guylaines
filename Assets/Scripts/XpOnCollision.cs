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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !shipControl.isDead)
        {
            XpInformation XpInfo = collision.gameObject.GetComponent<XpInformation>();
            int XpType = XpInfo.XpType;
            if (XpType > 0)
            {

                float distX = Mathf.Abs(/*collision.gameObject.transform.position.x*/collision.contacts[0].point.x - playerObj.transform.position.x);
                float xpStrength = Time.deltaTime * 10 * (27.0f - distX);
                if (distX < 15)
                {
                    xpStrength += 50;
                    if (distX < 8)
                        xpStrength += 250;
                }
                //Debug.Log(distX);
                switch (XpType)
                {
                    case 1:
                        StatManager.Instance.Speed.addXP(xpStrength);
                        break;
                    case 2:
                        StatManager.Instance.Handling.addXP(xpStrength);
                        break;
                    case 3:
                        StatManager.Instance.Energy.addXP(xpStrength);
                        break;

                }
            }

        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !shipControl.isDead)
        {
            XpInformation XpInfo=collision.gameObject.GetComponent<XpInformation>();
            int XpType = XpInfo.XpType;
            if (XpType > 0)
            {
                switch(XpType)
                {
                    case 1:
                        StatManager.Instance.Speed.addXP(25);
                        break;
                    case 2:
                        StatManager.Instance.Handling.addXP(25);
                        break;
                    case 3:
                        StatManager.Instance.Energy.addXP(25);
                        break;

                }
            }

        }

    }
}
