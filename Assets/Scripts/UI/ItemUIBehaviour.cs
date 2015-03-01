using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ItemUIBehaviour : MonoBehaviour {
	private TweenParams _rotationTweenParams;

	private Slider _speedWidget;
	private Slider _handlingWidget;
	private Slider _energyWidget;

	void Awake () {
		Transform graph = transform.Find ("Canvas").Find ("Background").Find ("Graph");
		_speedWidget = graph.Find ("Speed").GetComponent<Slider>();
		_handlingWidget = graph.Find ("Handling").GetComponent<Slider>();
		_energyWidget = graph.Find ("Energy").GetComponent<Slider>();

		_rotationTweenParams = new TweenParams ().SetEase (Ease.OutCubic);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.T)) {
			OpenUI ();
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			CloseUI ();
		}
	}

	public void OpenUI() {
		transform.DORotate (new Vector3 (0, 20, 0), 0.5f, 0).SetAs (_rotationTweenParams);
	}

	public void CloseUI() {
		transform.DORotate (new Vector3 (0, 90, 0), 0.5f);
	}
}
