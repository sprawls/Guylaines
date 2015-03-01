using UnityEngine;
using System.Collections.Generic;
using Rand = System.Random;

public class ChunkObject : MonoBehaviour
{
    public int cost;
    public bool sameXZ;
    public bool sameXY;
    public bool cannotRotate;

    public void Awake()
    {
        int floorLayerMask = 1 << 10;
        Vector3 pos = transform.position;
        pos.y += 20;
        Ray groundingRay = new Ray(pos, -transform.up);
        RaycastHit hit;
        Vector3 surfaceNormal = new Vector3(0, 0, 0);
        Vector3 fwd = transform.forward;

        if (Physics.Raycast(groundingRay, out hit, 25f, floorLayerMask))
        {
            transform.position = hit.point;
            surfaceNormal = hit.normal;
        }

        if(!cannotRotate)
        {
            
            Vector3 proj = fwd - (Vector3.Dot(fwd, hit.normal)) * hit.normal;
            
            Quaternion q=  Quaternion.LookRotation(proj, hit.normal);
            transform.rotation = q;
        }   
       
    }

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
