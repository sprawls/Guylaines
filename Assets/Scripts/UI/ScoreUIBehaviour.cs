using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUIBehaviour : MonoBehaviour {
	
	private SectionSlider _runsSlider;
	private Text _nextRunText;

	private Text _distanceText;
	private Text _bestText;


	public ScoreUIBehaviour Instance { get; private set; }

	void Awake() {
		Instance = this;
	}

	public float RunsRemaining {
		set {
			_runsSlider.value = value;
		}
	}

	public float ToNextRun {
		set {
			_nextRunText.text = value.ToString ("F0");
		}
	}

	public float Distance {
		set {
			_distanceText.text = value.ToString("F0");
		}
	}

	public float Best {
		set {
			_bestText.text = value.ToString ("F0");
		}
	}
}
