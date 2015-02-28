using UnityEngine;
using System.Collections;

public class XpInformation : MonoBehaviour {
    private int _xpType = 0;
	// Use this for initialization
	void Start () {
        _xpType = Random.Range(0, 3);
	}

    public int XpType
    {
        get { return this._xpType; }
        set { this._xpType = value; }
    } 
	
	// Update is called once per frame
	void Update () {
	
	}
}
