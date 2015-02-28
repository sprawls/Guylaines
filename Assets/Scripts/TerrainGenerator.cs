using UnityEngine;
using System.Collections.Generic;

using ChunkPair = Pair<Chunk,Chunk>;

public class TerrainGenerator : MonoBehaviour 
{
    //Initialisation variable
    public int seed;

    public float sizeChunk = 50;
    public float loadDistance = 20;

    //Reference to the player
    private GameObject player;

    public List<GameObject> availablePrefab;

    private List<ChunkPair>.Enumerator currentChunk;

    private List<ChunkPair> chunks = new List<ChunkPair>();

	// Use this for initialization
	void Start () {
        Random.seed = seed;
        player = GameObject.Find("Ship Prefab");
        Chunk c = Chunk.Create(new Vector2(-sizeChunk / 2, 0), new Vector2(sizeChunk / 2, sizeChunk));
        ChunkPair cp = new ChunkPair();
        cp.First = c;
        chunks.Add(cp);
        currentChunk = chunks.GetEnumerator();
        currentChunk.MoveNext();
	}
	
	// Update is called once per frame
	void Update () {
        generateLeftChunkIfNeeded();
        generateRightChunckIfNeeded();
        generateFrontChunckIfNeeded();

        switchLayer();
	}

    void generateLeftChunkIfNeeded()
    {
        // currentChunk = Chunk.Create(new Vector2(-250, 0), 500);
    }
    void generateRightChunckIfNeeded()
    {

    }
    void generateFrontChunckIfNeeded()
    {

    }
    void switchLayer()
    {

    }
}
