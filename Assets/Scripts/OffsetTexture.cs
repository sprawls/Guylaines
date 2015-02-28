using UnityEngine;
using System.Collections;

public class OffsetTexture : MonoBehaviour {
    float offset = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        offset+=0.01f;
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        Debug.Log(offset);
        mesh.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.0f));
        mesh.material.SetTextureOffset("_BumpMap", new Vector2(offset, 0.0f));
	}
}
