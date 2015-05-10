using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LerpImageAlpha : MonoBehaviour {

    /// <summary>
    /// Lerps the alpha of the image component attached to this gameobject
    /// </summary>

    public float startAlpha = 0f;
    public float endAlpha = 1f;
    public float time = 2f;
    public bool loops = true;

    private Image image;

	// Use this for initialization
    void Awake() {
        StartLerp();
    }


    void OnEnable() {
        Debug.Log("FSDDSFSDFS");
        StopAllCoroutines();
        StartLerp();
    }

    void StartLerp() {
        image = GetComponent<Image>();
        if (image != null) {
            Color s = image.color;
            s.a = startAlpha;
            Color e = image.color;
            e.a = endAlpha;
            StartCoroutine(LerpAlpha(s, e));
        }
    }

    IEnumerator LerpAlpha(Color s, Color e) {

        for (float i = 0; i < 1; i += Time.deltaTime / time) {
            image.color = Color.Lerp(s,e,i);
            yield return null;
        }
        image.color = e;

        if(loops) {
            StartCoroutine(LerpAlpha(e,s));
        }
    }
}
