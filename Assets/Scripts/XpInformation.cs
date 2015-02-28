using UnityEngine;
using System.Collections;

public class XpInformation : MonoBehaviour {
    private int _xpType = 0;
    private Light lightObj;
    private MeshRenderer meshRend;
	// Use this for initialization
	void Start () {
        _xpType = Random.Range(0, 4);
        lightObj = GetComponent<Light>();
        meshRend = GetComponent<MeshRenderer>();
        switch (_xpType)
        {
            case 1:
                lightObj.color = Color.red;
                meshRend.material.color = Color.red;
                break;
            case 2:
                lightObj.color = Color.blue;
                meshRend.material.color = Color.blue;
                break;
            case 3:
                lightObj.color = Color.yellow;
                meshRend.material.color = Color.yellow;
                break;
        }
	}

    public int XpType
    {
        get { return this._xpType; }
        set { this._xpType = value; }
    }

    public void glow(float value)
    {
        lightObj.intensity = 10+(value);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
