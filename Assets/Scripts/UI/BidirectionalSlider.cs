using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BidirectionalSlider : MonoBehaviour {

	private Slider _negativeSlider;
	private Slider _positiveSlider;

	[Range(-1, 1)]
	public float value;	

	// Use this for initialization
	void Awake () {
		_negativeSlider = transform.Find ("Negative").GetComponent<Slider>();
		_positiveSlider = transform.Find ("Positive").GetComponent<Slider>();

		UpdateValues ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateValues ();
	}

	private void UpdateValues() {
		_positiveSlider.value = Mathf.Max (0, this.value);
		_negativeSlider.value = Mathf.Max (0, -this.value);
	}
}
