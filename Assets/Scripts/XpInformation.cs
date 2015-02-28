using UnityEngine;
using System.Collections;

public class XpInformation : MonoBehaviour {
    private int _xpType = 0;
    private Light lightObj;
    private MeshRenderer meshRend;
	// Use this for initialization
	void Start () {
        _xpType = Random.Range(0, 4);
        lightObj = GetComponentInChildren<Light>();
        meshRend = GetComponentInChildren<MeshRenderer>();
        Color col = Color.white;
        switch (_xpType)
        {
            case 1:
                col = Color.blue;
                break;
            case 2:
                col = Color.green;
                break;
            case 3:
                col = Color.yellow;
                break;
        }
        lightObj.color = col;
        meshRend.material.color = col;
	}

    public int XpType
    {
        get { return this._xpType; }
        set { this._xpType = value; }
    }

    public void glow(float value)
    {
        lightObj.intensity = 10+(value*5);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
