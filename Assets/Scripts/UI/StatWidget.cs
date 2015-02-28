using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatWidget : MonoBehaviour {
	
	private Text _levelText;
	private Text _multiplierText;
	private Slider _xpSlider;

	public void Awake() {
		_levelText = transform.Find ("Level").GetComponent<Text>();
		_multiplierText = transform.Find ("Left").Find ("Multiplier").GetComponent<Text>();
		_xpSlider = transform.Find ("Left").Find ("Slider").GetComponent<Slider>();
	}
	
	public int level {
		set { _levelText.text = value.ToString("D4"); }
	}

	public float multiplier {
		set {
			float multi = Mathf.Round (value * 10f) / 10f;
			_multiplierText.text = multi.ToString();
		}
	}

	public void setXP (int value) {
		float xpProgress = (float)value / Stat.XP_TO_LEVEL;
		_xpSlider.value = xpProgress;
	}
}
