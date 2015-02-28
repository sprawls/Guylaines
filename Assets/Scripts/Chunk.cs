using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

    private Vector2 _bottomLeft;
    private Vector2 _topRight;
    private TerrainGenerator tg;
	// Use this for initialization

    public GameObject floor;
    public List<ChunkObject> availablePrefab;
    
    public int layerMin;
    public int currentLayer;
    
	void Start () {
        name = "Chunk";
        tg = FindObjectOfType<TerrainGenerator>();

        AddFloor();
        PopulateChunk(); 
	}

    virtual public void AddFloor()
    {
        GameObject plane = Instantiate(floor) as GameObject;
        Vector2 middle = (_bottomLeft + _topRight) / 2;
        Vector2 scale = (_topRight - _bottomLeft) / 10; //Je comprend que dale pourquoi mais cela arrive...

        plane.transform.localPosition = new Vector3(middle.x, -4, middle.y);
        plane.transform.localScale = new Vector3(scale.x, 1, scale.y);

        plane.transform.parent = transform;
    }

    virtual public void PopulateChunk()
    { 
        int count = availablePrefab.Count;
        int maxPoints = currentLayer * 20 + 100;
        
        int points = 0;
        while(points<maxPoints)
        {

            ChunkObject go = Instantiate(availablePrefab.OneAtRandom(tg.rand)) as ChunkObject;
            
            float x = tg.rand.Range(_bottomLeft.x, _topRight.x);
            float y = tg.rand.Range(_bottomLeft.y, _topRight.y);

            
            
            go.transform.localPosition = new Vector3(x, 0, y);
            go.transform.localScale = new Vector3(tg.rand.Next(5, 15), tg.rand.Next(5,35), tg.rand.Next(0, 15));
            go.transform.parent = transform;

            points += go.cost;
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
