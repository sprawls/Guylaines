using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class SectionSlider : MonoBehaviour {

	[Range(0, 10)]
	public float value;

	public Color notFullColor;
	public Color fullColor;

	private Slider[] _sliders;

	void Awake() {
		_sliders = transform.GetComponentsInChildren<Slider> ();
	}

	void Update() {
		UpdateValues();
	}

	private void UpdateValues() {
		float tempValue = value;

		foreach(Slider slider in _sliders) {
			Image image = slider.fillRect.GetComponent<Image>();

			slider.value = Mathf.Min (1, tempValue);

			if (slider.value == 1) {
				image.color = fullColor;
			} else {
				image.color = notFullColor;
			}
			tempValue -= 1;
		}
	}

	public void TweenRunsOnRunEnd() {
		DOTween.To (() => value, x => value = x, value - 1, 0.3f).SetEase (Ease.OutCubic);
	}
}
