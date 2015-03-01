using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class StatWidget : MonoBehaviour {
	
	private Text _levelText;
	private Text _multiplierText;
	private Slider _xpSlider;

	private float _currentXP;
	private int _currentLevel;
	private Color _initialColor;
	private Color _currentColor;
	private TweenParams _tParams;

	public void Awake() {
		_levelText = transform.Find ("Level").GetComponent<Text>();
		_multiplierText = transform.Find ("Left").Find ("Multiplier").GetComponent<Text>();
		_xpSlider = transform.Find ("Left").Find ("Slider").GetComponent<Slider>();

		_tParams = new TweenParams ().SetEase (Ease.OutCubic);
		_initialColor = _levelText.color;
	}

	public void Update() {
		_xpSlider.value = (_currentXP % Stat.XP_TO_LEVEL) / Stat.XP_TO_LEVEL;
	}
	
	public int level {
		set {
			_levelText.text = value.ToString("D4");
			_levelText.color = Color.white;
			_levelText.DOColor (_initialColor, 0.2f);
		}
	}

	public float multiplier {
		set {
			float multi = Mathf.Round (value * 10f) / 10f;
			_multiplierText.text = multi.ToString();
		}
	}

	public void setXP (int value) {
		DOTween.To (x => _currentXP = x, _currentXP, value, 0.5f).SetAs(_tParams);
	}
}
