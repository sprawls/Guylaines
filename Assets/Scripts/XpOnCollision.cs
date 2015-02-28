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
        if (collision.gameObject.tag == "Obstacle")
        {
            XpInformation XpInfo=collision.gameObject.GetComponent<XpInformation>();
            int XpType = XpInfo.XpType;
            if (XpType > 0)
            {
                float distX = Mathf.Abs(collision.gameObject.transform.position.x - playerObj.transform.position.x);
                float xpStrength = Time.deltaTime*10*(25.0f - distX);
                switch(XpType)
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
}
