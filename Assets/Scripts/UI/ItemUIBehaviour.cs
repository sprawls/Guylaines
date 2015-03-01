using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ItemUIBehaviour : MonoBehaviour {
	public Color normalColor;
	public Color selectedColor;
	public float pickAnimDuration;
	public float selectedZ;

	private TweenParams _rotationTweenParams;

	private Slider _speedWidget;
	private Slider _handlingWidget;
	private Slider _energyWidget;

	private Image _heldItemPanel;
	private Image _newItemPanel;

	private bool _uiIsActive = false;

	private float _heldZ = 0;
	private float _newZ = 0;

	void Awake () {
		Transform contentPanel = transform.Find ("Canvas").Find ("Content Panel");
		_heldItemPanel = contentPanel.Find ("Held Item Panel").GetComponent<Image>();
		_newItemPanel = contentPanel.Find ("New Item Panel").GetComponent<Image>();

		Transform graph = _newItemPanel.transform.Find ("Graph");
		_speedWidget = graph.Find ("Speed").GetComponent<Slider>();
		_handlingWidget = graph.Find ("Handling").GetComponent<Slider>();
		_energyWidget = graph.Find ("Energy").GetComponent<Slider>();

		_rotationTweenParams = new TweenParams ().SetEase (Ease.OutCubic);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.T)) {
			OpenUI ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			CloseUI ();
		}

		if (_uiIsActive) {
			HandleInputs();
		}
	}

	public void OpenUI() {
		transform.DORotate (new Vector3 (0, 20, 0), 0.5f, 0).SetAs (_rotationTweenParams);
		_uiIsActive = true;
		PickNewItem ();
	}

	public void CloseUI() {
		transform.DORotate (new Vector3 (0, 90, 0), 0.5f);
		_uiIsActive = false;
	}

	private void HandleInputs() {
		if (Input.GetKeyDown (KeyCode.A)) {
			PickHeldItem();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			PickNewItem();
		}
	}

	private void PickHeldItem() {
		Vector3 heldPos = _heldItemPanel.rectTransform.anchoredPosition3D;
		Vector3 newPos = _newItemPanel.rectTransform.anchoredPosition3D;

		_heldItemPanel.DOColor (selectedColor, pickAnimDuration);
		_heldItemPanel.rectTransform.DOAnchorPos3D (new Vector3(heldPos.x, heldPos.y, -selectedZ), pickAnimDuration);

		_newItemPanel.DOColor (normalColor, pickAnimDuration);
		_newItemPanel.rectTransform.DOAnchorPos3D (new Vector3(newPos.x, newPos.y, 0), pickAnimDuration);
	}

	private void PickNewItem() {
		Vector3 heldPos = _heldItemPanel.rectTransform.anchoredPosition3D;
		Vector3 newPos = _newItemPanel.rectTransform.anchoredPosition3D;

		_heldItemPanel.DOColor (normalColor, pickAnimDuration);
		_heldItemPanel.rectTransform.DOAnchorPos3D (new Vector3(heldPos.x, heldPos.y, 0), pickAnimDuration);

		_newItemPanel.DOColor (selectedColor, pickAnimDuration);
		_newItemPanel.rectTransform.DOAnchorPos3D (new Vector3(newPos.x, newPos.y, -selectedZ), pickAnimDuration);
	}
}
