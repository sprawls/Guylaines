using UnityEngine;
using System.Collections;

public class GrowErect : MonoBehaviour {

	public float rate;
	public float fadeTime;

	MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshRenderer>();
		StartCoroutine (FadeOut(fadeTime));
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x < 1000000)
        {
            transform.localScale += new Vector3(rate * transform.localScale.x, rate / 4f * transform.localScale.y, rate * transform.localScale.z);
            rate += rate * 0.02f;
            //transform.localScale *= rate;
        }
	}

	private IEnumerator FadeOut(float fadeTime) {
		Color startColor = mesh.material.GetColor("_TintColor");
		Color endColor = new Color(startColor.r,startColor.g,startColor.b,0);

		for(float i = 0; i < 1; i+= Time.deltaTime/fadeTime) {
			mesh.material.SetColor("_TintColor", Color.Lerp (startColor,endColor,i));
			yield return null;
		}
	}
}
