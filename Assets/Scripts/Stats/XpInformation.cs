using UnityEngine;
using System.Collections;

public class XpInformation : MonoBehaviour {
    private int _xpType = 0;
    private MeshRenderer meshRend;
	// Use this for initialization
	void Start () {
        _xpType = Random.Range(0, 4);
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
			case 0:
				Destroy (this);
				break;

        }
        meshRend.material.color = col;
	}

    public int XpType
    {
        get { return this._xpType; }
        set { this._xpType = value; }
    }

	public Color getColor() {
		return(meshRend.material.color);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
