using UnityEngine;
using System.Collections;

public class FollowPlayerSpeed : MonoBehaviour {

    private ShipControl ship;

	// Use this for initialization
    void Awake() {
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(ship != null && ship.isActiveAndEnabled) FollowPlayer();
	}

    void FollowPlayer() {
        //float speed = (ship.forwardSpeed + ship.additionalSpeed) * Time.deltaTime * ship.SpeedDeltaTimeAdjuster;
        float speed = ship.getForwardSpeed();
        speed = speed * Time.deltaTime * ship.SpeedDeltaTimeAdjuster;
        transform.position += new Vector3(0, 0, speed);

    }
}
