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

    private int index;

    private List<ChunkPair> chunks = new List<ChunkPair>();

	// Use this for initialization
	void Start () {
        Random.seed = seed;
        player = GameObject.Find("Ship Prefab");

        Chunk c = Chunk.Create(new Vector2(-sizeChunk, 0), new Vector2(0, sizeChunk));
        ChunkPair cp = new ChunkPair();
        cp.First = c;
        chunks.Add(cp);

        c = Chunk.Create(new Vector2(0, 0), new Vector2(sizeChunk, sizeChunk));
        cp = new ChunkPair();
        cp.First = c;
        chunks.Add(cp);
        index = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        generateLeftChunkIfNeeded();
        generateRightChunckIfNeeded();
        generateFrontChunckIfNeeded();
        findCurrent();
        switchLayer();
	}

    //Update index so that it point to the chunk which contain the player
    void findCurrent()
    {
        Vector3 pos3 = player.transform.localPosition;
        float x = pos3.x;

        while(x < currentChunk.First.left)
        {
            index--;
        }

        while(x > currentChunk.First.right)
        {
            index++;
        }
    }

    void generateLeftChunkIfNeeded()
    {
        ChunkPair c = currentChunk;
        ChunkPair l = leftChunk;
        bool updated = false;
        bool shouldUpdate = c.First.left > player.transform.localPosition.x - loadDistance;

        if (l.First == null && shouldUpdate)
        {
            Vector2 move = new Vector2(-sizeChunk, 0);
            Vector2 bl = c.First.bottomLeft;
            Vector2 tr = c.First.topRight;

            bl += move;
            tr += move;

            l.First = Chunk.Create(bl, tr);
            updated = true;
        }

        if(l.Second == null && shouldUpdate && !updated)
        {
            Vector2 move = new Vector2(0, sizeChunk);

            Vector2 bl = l.First.bottomLeft + move;
            Vector2 tr = l.First.topRight + move;

            l.Second = Chunk.Create(bl, tr);
            updated = true;
        }

        if(updated)
        {
            leftChunk = l;
        }
    }
    void generateRightChunckIfNeeded()
    {
        ChunkPair c = currentChunk;
        ChunkPair r = rightChunk;
        bool shouldUpdate = c.First.right < player.transform.localPosition.x + loadDistance;
        bool updated = false;

        if (r.First == null && shouldUpdate)
        {
            Vector2 move = new Vector2(sizeChunk, 0);
            Vector2 bl = c.First.bottomLeft;
            Vector2 tr = c.First.topRight;

            bl += move;
            tr += move;

            r.First = Chunk.Create(bl, tr);
            updated = true;
        }

        if (r.Second == null && shouldUpdate && !updated)
        {
            Vector2 move = new Vector2(0, sizeChunk);

            Vector2 bl = r.First.bottomLeft + move;
            Vector2 tr = r.First.topRight + move;

            r.Second = Chunk.Create(bl, tr);
            updated = true;
        }

        if (updated)
        {
            rightChunk = r;
        }
    }
    void generateFrontChunckIfNeeded()
    {
        ChunkPair c = currentChunk;
        Chunk f = c.First;
        if (c.Second == null && f.top < player.transform.localPosition.z + loadDistance)
        {
            c.Second = Chunk.Create(new Vector2(f.left, f.bottom + +sizeChunk), new Vector2(f.right, f.top + sizeChunk));
            currentChunk = c;
        }
    }
    void switchLayer()
    {
        if(player.transform.localPosition.z > currentChunk.First.top)
        {
            chunks = chunks.ConvertAll(delegate(ChunkPair p)
            {
                if(p.First != null)
                {
                    Destroy(p.First.gameObject);
                }
                p.First = p.Second;
                p.Second = null;
                return p;
            });
        }
    }

    public ChunkPair currentChunk
    {
        get
        {
            return chunks[index];
        }
        set
        {
            chunks[index] = value;
        }
    }

    public ChunkPair rightChunk
    {
        get
        {
            if (index + 1 < chunks.Count)
            {
                return chunks[index + 1];
            }
            else
            {
                return new ChunkPair();
            }
        }

        set
        {
            if (index + 1 < chunks.Count)
            {
                chunks[index + 1] = value;
            }
            else
            {
                chunks.Add(value);
            }
        }
    }

    public ChunkPair leftChunk
    {
        get
        {
            if(index > 0)
            {
                return chunks[index - 1];
            }
            else
            {
                return new ChunkPair();
            }
        }

        set
        {
            if (index > 0)
            {
                chunks[index - 1] = value;
            }
            else
            {
                chunks.Insert(0, value);
                index++;
            }
        }
    }
}
