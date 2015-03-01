using UnityEngine;
using Rand = System.Random;
using System.Collections.Generic;
using System.Linq;

using ChunkPair = Pair<Chunk,Chunk>;

public class TerrainGenerator : MonoBehaviour 
{
    //Initialisation variable
    public int seed;
    public Rand rand { get; private set; }
    public int forcedWidth;

    public float sizeChunk = 50;
    public float loadDistance = 20;

    //Reference to the player
    private GameObject player;

    public  List<Chunk> ChunkSpecifier;

    private int index;
    


    public int layer = 0;

    private List<ChunkPair> chunks = new List<ChunkPair>();

    void Awake()
    {
        rand = new Rand(seed);
    }
	// Use this for initialization
	void Start () {
        
        player = GameObject.Find("Ship Prefab");
        ChunkSpecifier.Sort(delegate(Chunk c1, Chunk c2)
        {
            if (c1.layerMin < c2.layerMin) return -1;
            if (c1.layerMin == c2.layerMin) return 0;
            return 1;
        });

        initChunk();
	}

    void initChunk()
    {
        if(forcedWidth > 0)
        {
            for(int i = -forcedWidth/2; i < forcedWidth / 2; i++)
            {
                Chunk c = Create(new Vector2(sizeChunk*i, 0), new Vector2(sizeChunk * (i+1), sizeChunk));
                ChunkPair cp = new ChunkPair();
                cp.First = c;
                cp.Second = Create(new Vector2(sizeChunk * i, sizeChunk), new Vector2(sizeChunk * (i + 1), sizeChunk * 2));
                chunks.Add(cp);
            }
        }
        else
        {
            Chunk c = Create(new Vector2(-sizeChunk, 0), new Vector2(0, sizeChunk));
            ChunkPair cp = new ChunkPair();
            cp.First = c;
            chunks.Add(cp);

            c = Create(new Vector2(0, 0), new Vector2(sizeChunk, sizeChunk));
            cp = new ChunkPair();
            cp.First = c;
            chunks.Add(cp);
            index = 1;
        }
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

            l.First = Create(bl, tr);
            updated = true;
        }

        if(l.Second == null && shouldUpdate && !updated)
        {
            Vector2 move = new Vector2(0, sizeChunk);

            Vector2 bl = l.First.bottomLeft + move;
            Vector2 tr = l.First.topRight + move;

            l.Second = Create(bl, tr,true);
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

            r.First = Create(bl, tr);
            updated = true;
        }

        if (r.Second == null && shouldUpdate && !updated)
        {
            Vector2 move = new Vector2(0, sizeChunk);

            Vector2 bl = r.First.bottomLeft + move;
            Vector2 tr = r.First.topRight + move;

            r.Second = Create(bl, tr,true);
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
            c.Second = Create(new Vector2(f.left, f.bottom + +sizeChunk), new Vector2(f.right, f.top + sizeChunk), true);
            currentChunk = c;
        }
    }
    void switchLayer()
    {
        if(player.transform.position.z -20 > currentChunk.First.top)
        {
            chunks = chunks.ConvertAll(ChunkSwitcher);
            layer++;
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

    private System.Converter<ChunkPair,ChunkPair> ChunkSwitcher
    {
        get
        {
            if(forcedWidth > 0)
            {
                Vector2 move = new Vector2(0, sizeChunk);
                return delegate(ChunkPair p)
                {
                    if (p.First != null)
                    {
                        Destroy(p.First.gameObject);
                    }
                    p.First = p.Second;
                    p.Second = Create(p.First.bottomLeft + move, p.First.topRight + move);

                    return p;
                };
            }
            else
            {
                return delegate(ChunkPair p)
                {
                    if (p.First != null)
                    {
                        Destroy(p.First.gameObject);
                    }
                    p.First = p.Second;

                    p.Second = null;
                    return p;
                };
            }
        }
    }

    private Chunk Create(Vector2 bottomLeft, Vector2 topRight)
    {
        return Create(bottomLeft, topRight, false);
    }
    private Chunk Create(Vector2 bottomLeft, Vector2 topRight, bool nextLayer)
    {
        int cur = nextLayer?layer+1:layer;
        Chunk c = Instantiate(LegalChunk(cur).OneAtRandom(rand)) as Chunk;

        c.SetBound(bottomLeft, topRight);
        c.currentLayer = cur;

        return c;
    }

    private int lastOkChunk;
    private List<Chunk> LegalChunk(int layer)
    {

        ChunkSpecifier = ChunkSpecifier.Where(delegate(Chunk c)
        {
            return layer <= c.layerMax || c.layerMax < 0;
        }).ToList();

        return ChunkSpecifier.TakeWhile(delegate(Chunk c)
        {
            return layer >= c.layerMin;
        }).ToList();
        
    }
}
