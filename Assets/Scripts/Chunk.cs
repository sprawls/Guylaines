using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

    private Vector2 _bottomLeft;
    private Vector2 _topRight;
    public TerrainGenerator tg { get; private set; }
	// Use this for initialization

    public List<Floor> floors;
    public List<Item> availableItem;
    public List<ChunkObject> availablePrefab;

    public int NbOfUsedPrefab;
    public int layerMin;
    public int layerMax;
    public int currentLayer;
    public int maxPoints;

    private bool populated = false;
    
	void Start () {
        name = "Chunk";
        tg = FindObjectOfType<TerrainGenerator>();
        Vector3 pos = new Vector3(left, 0, bottom);
        transform.position = pos;
        AddFloor();
        maxPoints = currentLayer * 2 + 100;
	}

    void Update()
    {
        if(!populated)
        {
            StartCoroutine(PopulateChunk());
            StartCoroutine(PopulateItem());
            populated = true;
        }   
    }

    virtual public void AddFloor()
    {
        Floor plane = Instantiate(floors.OneAtRandom(tg.rand)) as Floor;
        Vector2 middle = (_bottomLeft + _topRight) / 2;
        Vector2 scale = (_topRight - _bottomLeft);

        plane.middle = middle;
        plane.size = scale;

        plane.transform.parent = transform;
    }

    virtual public IEnumerator PopulateItem()
    {
        if(availableItem.Count <= 0)
        {
            yield return null;
        }
        else
        {

            for (int i = 0; i < 3; i++)
            {

                Item go = Instantiate(availableItem.OneAtRandom(tg.rand)) as Item;
                go.basePower = currentLayer + 1;

                float x = tg.rand.Range(_bottomLeft.x, _topRight.x);
                float y = tg.rand.Range(_bottomLeft.y, _topRight.y);

                go.transform.localPosition = new Vector3(x, 0, y);
                go.transform.parent = transform;
                yield return null;
            }
        }
    }

    virtual public IEnumerator PopulateChunk()
    {
        if(availableItem.Count <= 0)
        {
            yield return null;
        }
        else
        {
            
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
                yield return null;
            }
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
