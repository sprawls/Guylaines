using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatWidget : MonoBehaviour {

	public Text levelText;
	public Text multiplierText;
	public Slider xpSlider;

	public void Awake() {
		levelText = transform.Find ("Level").GetComponent<Text>();
		multiplierText = transform.Find ("Left").Find ("Multiplier").GetComponent<Text>();
		xpSlider = transform.Find ("Left").Find ("Slider").GetComponent<Slider>();
	}
	
	public int level {
		set { levelText.text = value.ToString("D4"); }
	}

	public float multiplier {
		set {
			float multi = Mathf.Round (value * 10f) / 10f;
			multiplierText.text = multi.ToString();
		}
	}

	public void setXP (int value, int max) {
		float xpProgress = (float)value / max;
		xpSlider.value = xpProgress;
	}
}
