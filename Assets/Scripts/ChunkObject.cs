using UnityEngine;
using System.Collections.Generic;
using Rand = System.Random;

public class ChunkObject : MonoBehaviour
{
    public int cost;
    public bool sameXZ;
    public bool sameXY;

    public void ScaleToSomethingFun(Rand r, float min, float max)
    {
        float x = r.Range(min, max);
        float y;
        float z;
        if(sameXY)
        {
            y = x;
        }
        else
        {
            y = r.Range(min, max);
        }
        if(sameXZ)
        {
            z = x;
        }
        else
        {
            z = r.Range(min, max);
        }
        transform.localScale = new Vector3(x, y, z);
    }
}
