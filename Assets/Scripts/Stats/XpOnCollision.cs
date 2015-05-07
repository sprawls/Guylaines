using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XpOnCollision : MonoBehaviour {
	
    public GameObject GlowParticul;
    private ShipControl shipControl;
    private GameObject playerObj;
	
	private XpLink xpLink; //Creates link inbetween ship and objects giving xp to add feedback

    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        shipControl = playerObj.GetComponentInChildren<ShipControl>();
		xpLink = GetComponent<XpLink> ();
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Obstacle" && !shipControl.isDead) {
			Color c = collision.gameObject.GetComponent<XpInformation>().getColor();
			xpLink.AddObjectToList(collision.collider.gameObject,c);
		}
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
                ////Debug.Log(distX);
                switch (XpType)
                {
                    case 1:
                        StatManager.Instance.Speed.addXP(xpStrength);
                        break;
                    case 2:
                        StatManager.Instance.Handling.addXP(xpStrength);
                        break;
                    case 3:
                        int levelsEarned = StatManager.Instance.Energy.addXP(xpStrength);
						StatManager.Instance.AddToCollectedEnergy(levelsEarned);
                        break;
                }
            }

        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !shipControl.isDead)
        {
			xpLink.RemoveObjectToList(collision.collider.gameObject); //Remove GameObject from colliding obj

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
