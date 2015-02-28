using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

    private Vector2 _bottomLeft;
    private Vector2 _topRight;
    private TerrainGenerator tg;
	// Use this for initialization

    public List<GameObject> availablePrefab;
    public int layerMin = 0;
    
	void Start () {
        name = "Chunk";
        tg = FindObjectOfType<TerrainGenerator>();

        AddFloor();
        PopulateChunk(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    virtual public void AddFloor()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Vector2 middle = (_bottomLeft + _topRight) / 2;
        Vector2 scale = (_topRight - _bottomLeft) / 10; //Je comprend que dale pourquoi mais cela arrive...

        plane.transform.localPosition = new Vector3(middle.x, -4, middle.y);
        plane.transform.localScale = new Vector3(scale.x, 1, scale.y);

        plane.transform.parent = transform;
    }

    virtual public void PopulateChunk()
    { 
        int count = availablePrefab.Count;
        for (int i = 0; i < 100; i++)
        {
            GameObject go = Instantiate(availablePrefab[i % 2]) as GameObject;
            
            float x = tg.rand.Range(_bottomLeft.x, _topRight.x);
            float y = tg.rand.Range(_bottomLeft.y, _topRight.y);

            go.transform.localPosition = new Vector3(x, 0, y);
            go.transform.parent = transform;
        }
    }

    public void SetBound(Vector2 bl, Vector2 tr)
    {
        _bottomLeft = bl;
        _topRight = tr;
    }

    public float top { get { return _topRight.y; } }
    public float right { get { return _topRight.x; } }
    public float bottom { get { return _bottomLeft.y; } }
    public float left { get { return _bottomLeft.x; } }
    public Vector2 bottomLeft { get { return _bottomLeft; } }
    public Vector2 topRight { get { return _topRight; } }
}
