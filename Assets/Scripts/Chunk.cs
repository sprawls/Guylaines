using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

    private Vector2 _bottomLeft;
    private Vector2 _topRight;
	// Use this for initialization

    
	void Start () {
        name = "Chunk";

        addFloor();
        addElems(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void addFloor()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Vector2 middle = (_bottomLeft + _topRight) / 2;
        Vector2 scale = (_topRight - _bottomLeft) / 10; //Je comprend que dale pourquoi mais cela arrive...

        plane.transform.localPosition = new Vector3(middle.x, -4, middle.y);
        plane.transform.localScale = new Vector3(scale.x, 1, scale.y);

        plane.transform.parent = transform;
    }

    void addElems()
    {
        TerrainGenerator tg = FindObjectOfType<TerrainGenerator>();
        int count = tg.availablePrefab.Count;
        for (int i = 0; i < 100; i++)
        {
            GameObject go = Instantiate(tg.availablePrefab[i%2]) as GameObject;

            float x = Random.Range(_bottomLeft.x, _topRight.x);
            float y = Random.Range(_bottomLeft.y, _topRight.y);

            go.transform.localPosition = new Vector3(x, 0, y);
            go.transform.parent = transform;
        }
    }

    public static Chunk Create(Vector2 bottomLeft, Vector2 topRight)
    {
        GameObject newObject = new GameObject();
        Chunk yourObject = newObject.AddComponent<Chunk>();

        yourObject._bottomLeft = bottomLeft;
        yourObject._topRight = topRight;

        return yourObject;
    }

    public float top { get { return _topRight.y; } }
    public float right { get { return _topRight.x; } }
    public float bottom { get { return _bottomLeft.y; } }
    public float left { get { return _bottomLeft.x; } }
    public Vector2 bottomLeft { get { return _bottomLeft; } }
    public Vector2 topRight { get { return _topRight; } }
}
