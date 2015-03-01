﻿using UnityEngine;
using System.Collections;

public class MONOCHUNK : Chunk {

    public override void PopulateChunk()
    {
        Bounds b = new Bounds();
        ChunkObject go = Instantiate(availablePrefab.OneAtRandom(tg.rand)) as ChunkObject;
        b= go.gameObject.GetComponent<Collider>().bounds;
        Vector3 s = b.size;

        Debug.Log(s);

        float neededSize = right - left;

        Debug.Log(neededSize);




        go.transform.localPosition = new Vector3(left + neededSize / 2, 0, bottom + neededSize / 2);
        s.x = neededSize / s.x;
        s.z = neededSize / s.z;
        s.y = (s.x + s.z) /2 ;
        go.transform.localScale = s;

        go.transform.parent = transform;
    }
}
