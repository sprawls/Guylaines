using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public int basePower;

    public void Start()
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
            //Debug.Log("hit");
            //Debug.Log(hit.point);
            //Debug.Log(transform.position);

            pos = hit.point;
            pos.y = pos.y + 2;
            transform.position = pos;
            //Debug.Log(transform.position);
        }
    }
}
