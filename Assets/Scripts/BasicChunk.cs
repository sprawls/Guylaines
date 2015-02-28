using UnityEngine;
using System.Collections.Generic;

public class BasicChunk : Chunk
{
    public int freeZoneLength;


    override public void PopulateChunk()
    {
        int maxPoints = currentLayer * 20 + 100;

        int points = 0;
        while (points < maxPoints)
        {

            ChunkObject go = Instantiate(availablePrefab.OneAtRandom(tg.rand)) as ChunkObject;

            float x = tg.rand.Range(left, right);
            float y = tg.rand.Range(bottom + freeZoneLength, top);

            go.transform.localPosition = new Vector3(x, 0, y);
            go.ScaleToSomethingFun(tg.rand, 1, 3);
            go.transform.parent = transform;

            points += go.cost;
        }
    }
}
