using UnityEngine;
using System.Collections;

public class FollowPlayerSpeed : MonoBehaviour {

    private ShipControl ship;

	// Use this for initialization
	void Start () {
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
        if(ship != null) FollowPlayer();
	}

    void FollowPlayer() {
        float speed = (ship.forwardSpeed + ship.additionalSpeed) * Time.deltaTime * ship.SpeedDeltaTimeAdjuster;
        transform.Translate(new Vector3(0, 0, speed));
    }
}
