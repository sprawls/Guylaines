using UnityEngine;
using System.Collections;

public class ItemStats {

    private float _speedMulti = 1;
    private float _handleMulti = 1;
    private float _EnergieMulti = 1;
	
    public ItemStats(float speed,float handling,float energie)
    { 
        _speedMulti=speed;
        _handleMulti=handling;
        _EnergieMulti = energie;
    }
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
}
