using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    public Vector3 middle;
    public Vector2 size;

	// Use this for initialization
	void Start () {
        //Debug.Log("Size in floor:" + size.ToString());
        transform.position = new Vector3(middle.x, 0, middle.y);
        transform.localScale = new Vector3(size.x / 10  , 1, size.y/10);
	}
	
}
