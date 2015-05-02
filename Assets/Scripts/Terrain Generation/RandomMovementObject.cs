using UnityEngine;
using System.Collections;

public class RandomMovementObject : MonoBehaviour {

    public float speed;
    public float maxTurnSpeed;

    private float direction;

    private TerrainGenerator tg;

	// Use this for initialization
	void Start () {
        direction = Random.value * 360;
	}
	
	// Update is called once per frame
	void Update () {
        float delta = Time.deltaTime;

        float frameChange = delta * maxTurnSpeed;
        direction += (2 * Random.value - 1f) * frameChange;
       
        Vector3 angle = new Vector3(Mathf.Cos(direction),0,Mathf.Sin(direction));
        transform.position = transform.position + angle.normalized * speed;
	}
}
