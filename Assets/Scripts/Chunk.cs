using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

    private Vector2 _bottomLeft;
    private Vector2 _topRight;
    public TerrainGenerator tg { get; private set; }
	// Use this for initialization

    public GameObject floor;
    public List<Item> availableItem;
    public List<ChunkObject> availablePrefab;

    public int NbOfUsedPrefab;
    public int layerMin;
    public int layerMax;
    public int currentLayer;
    
	void Start () {
        name = "Chunk";
        tg = FindObjectOfType<TerrainGenerator>();

        AddFloor();
        PopulateChunk();
        PopulateItem();
	}

    virtual public void AddFloor()
    {
        GameObject plane = Instantiate(floor) as GameObject;
        Vector2 middle = (_bottomLeft + _topRight) / 2;
        Vector2 scale = (_topRight - _bottomLeft) / 10; //Je comprend que dale pourquoi mais cela arrive...

        plane.transform.localPosition = new Vector3(middle.x, 0, middle.y);
        plane.transform.localScale = new Vector3(scale.x, 1, scale.y);

        plane.transform.parent = transform;
    }

    virtual public void PopulateItem()
    {
        if(availableItem.Count <= 0)
        {
            return;
        }
        int maxPoints = currentLayer ;

        for (int i = 0; i < 5; i++)
        {

            Item go = Instantiate(availableItem.OneAtRandom(tg.rand)) as Item;
            go.basePower = currentLayer;

            float x = tg.rand.Range(_bottomLeft.x, _topRight.x);
            float y = tg.rand.Range(_bottomLeft.y, _topRight.y);

            go.transform.localPosition = new Vector3(x, 0, y);
            go.transform.parent = transform;
        }
    }

    virtual public void PopulateChunk()
    {
        if (availablePrefab.Count <= 0)
        {
            return;
        }
        int maxPoints = currentLayer * 20 + 100;
        
        int points = 0;
        while(points<maxPoints)
        {

            ChunkObject go = Instantiate(availablePrefab.OneAtRandom(tg.rand)) as ChunkObject;
            
            float x = tg.rand.Range(_bottomLeft.x, _topRight.x);
            float y = tg.rand.Range(_bottomLeft.y, _topRight.y);
            
            
            go.transform.localPosition = new Vector3(x, 0, y);
            go.ScaleToSomethingFun(tg.rand, 1, 4);
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
