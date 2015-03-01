using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunEndUIBehaviour : MonoBehaviour {
	public static RunEndUIBehaviour Instance { get; private set; }

	private Animator _animator;
	private Text _runsCaption;
	private Text _gameOverCaption;

	void Awake () {
		Instance = this;

		_runsCaption = transform.Find ("Runs").GetComponent<Text> ();
		_gameOverCaption = transform.Find ("GameOver").GetComponent<Text> ();
		_gameOverCaption.text = "";
		_animator = GetComponent<Animator> ();
	}
	
	public void OnRunEnd() {
		float runs = StatManager.Instance.RunsRemaining - 1;

		if (runs == 0) {
			_runsCaption.text = string.Format ("YOU MADE {0} DISTANCE", (int)StatManager.Instance.BestRun);
			_gameOverCaption.text = "GAME OVER";
		}
		else if (runs == 1) {
			_runsCaption.text = "FINAL RUN";
		} else {
			_runsCaption.text = string.Format ("{0} RUNS REMAINING", StatManager.Instance.RunsRemaining - 1);
		}

		_animator.SetTrigger("Run End");
	}
}
