using UnityEngine;
using System.Collections;

public class ItemStats {

    private float _speedMulti = 1;
    private float _handleMulti = 1;
    private float _EnergieMulti = 1;


    public float speedMulti
    {
        get { return this._speedMulti; }
        set { this._speedMulti = value; }
    }

    public float handleMulti
    {
        get { return this._handleMulti; }
        set { this._handleMulti = value; }
    }

    public float EnergieMulti
    {
        get { return this._EnergieMulti; }
        set { this._EnergieMulti = value; }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
