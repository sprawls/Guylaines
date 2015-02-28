using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

    private Vector2 bottomLeft;
    private Vector2 topRight;
	// Use this for initialization


	void Start () {
        TerrainGenerator tg = FindObjectOfType<TerrainGenerator>();
        Debug.Log("Start Chunk");
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(tg.availablePrefab[0]) as GameObject;
            
            float x = Random.Range(bottomLeft.x, topRight.x);
            float y = Random.Range(bottomLeft.y, topRight.y);

            go.transform.localPosition = new Vector3(x, 0, y);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static Chunk Create(Vector2 bottomLeft, Vector2 topRight)
    {
        GameObject newObject = new GameObject();
        Chunk yourObject = newObject.AddComponent<Chunk>();

        yourObject.bottomLeft = bottomLeft;
        yourObject.topRight = topRight;

        return yourObject;
    }
}
