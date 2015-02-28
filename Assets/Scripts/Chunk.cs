using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

    private Vector2 _bottomLeft;
    private Vector2 _topRight;
	// Use this for initialization


	void Start () {
        TerrainGenerator tg = FindObjectOfType<TerrainGenerator>();
        Debug.Log("Start Chunk");
        for (int i = 0; i < 100; i++)
        {
            GameObject go = Instantiate(tg.availablePrefab[0]) as GameObject;
            
            float x = Random.Range(_bottomLeft.x, _topRight.x);
            float y = Random.Range(_bottomLeft.y, _topRight.y);

            go.transform.localPosition = new Vector3(x, 0, y);
            go.transform.parent = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
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
