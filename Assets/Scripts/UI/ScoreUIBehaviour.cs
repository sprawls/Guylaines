using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUIBehaviour : MonoBehaviour {
	public static ScoreUIBehaviour Instance { get; private set; }

	private SectionSlider _runsSlider;
	private Text _nextRunText;

	private Text _distanceText;
	private Text _bestText;

	void Awake() {
		Instance = this;

		Transform livesWidget = transform.Find ("Screen Edges/LivesWidget");
		_runsSlider = livesWidget.Find ("SectionSlider").GetComponent<SectionSlider>();
		_nextRunText = livesWidget.Find ("ToNext").GetComponent<Text> ();

		Transform distanceWidget = transform.Find ("Screen Edges/DistanceWidget");
		_distanceText = distanceWidget.Find ("Distance").GetComponent<Text> ();
		_bestText = distanceWidget.Find ("Best").GetComponent<Text> ();
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
