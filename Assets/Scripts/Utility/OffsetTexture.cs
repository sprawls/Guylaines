using UnityEngine;
using System.Collections;

public class OffsetTexture : MonoBehaviour {
    float offset = 0.0f;
    public float rate = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        offset += rate * Time.deltaTime;
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.0f));
        mesh.material.SetTextureOffset("_BumpMap", new Vector2(offset, 0.0f));
	}
}
