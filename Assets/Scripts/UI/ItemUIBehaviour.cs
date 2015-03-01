using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ItemUIBehaviour : MonoBehaviour {
	public static ItemUIBehaviour Instance { get; private set; }

	public Color normalColor;
	public Color selectedColor;
	public float pickAnimDuration;
	public float selectedZ;

	private TweenParams _rotationTweenParams;

	private BidirectionalSlider _speedWidget;
	private BidirectionalSlider _handlingWidget;
	private BidirectionalSlider _energyWidget;

	private Image _heldItemPanel;
	private Image _newItemPanel;

	private bool _uiIsActive = false;
	private bool _newItemIsPicked = true;

	private ItemStats _newItem;

	void Awake () {
		Instance = this;

		Transform contentPanel = transform.Find ("Canvas").Find ("Content Panel");
		_heldItemPanel = contentPanel.Find ("Held Item Panel").GetComponent<Image>();
		_newItemPanel = contentPanel.Find ("New Item Panel").GetComponent<Image>();

		Transform graph = _newItemPanel.transform.Find ("Graph");
		_speedWidget = graph.Find ("Speed").GetComponent<BidirectionalSlider>();
		_handlingWidget = graph.Find ("Handling").GetComponent<BidirectionalSlider>();
		_energyWidget = graph.Find ("Energy").GetComponent<BidirectionalSlider>();

		_rotationTweenParams = new TweenParams ().SetEase (Ease.OutCubic).OnComplete (TweenItemValues);
	}

	void Update() {
		if (_uiIsActive) {
			HandleInputs();
		}
	}

	public void OpenUI(ItemStats newItem) {
		_newItem = newItem;

		_speedWidget.value = 0;
		_handlingWidget.value = 0;
		_energyWidget.value = 0;

		transform.DORotate (new Vector3 (0, 20, 0), 0.5f, 0).SetAs (_rotationTweenParams);
		_uiIsActive = true;
		PickNewItem ();
	}

	public void CloseUI() {
		transform.DORotate (new Vector3 (0, 90, 0), 0.5f);
		_uiIsActive = false;
	}

	public void TweenItemValues() {
		float[] graphValues = new float[3];
		ItemStats currentItem = StatManager.Instance.ItemStat;

		graphValues [0] = (_newItem.speedMulti - currentItem.speedMulti);
		graphValues [1] = (_newItem.handleMulti - currentItem.handleMulti);
		graphValues [2] = (_newItem.EnergieMulti - currentItem.EnergieMulti);

		float maxDelta = 0;
		foreach (float graphValue in graphValues) {
			maxDelta = Mathf.Max (Mathf.Abs (graphValue), maxDelta);
		}
		for (int i = 0; i < graphValues.Length; i++) {
			graphValues[i] /= maxDelta;
		}

		Debug.Log (string.Format ("New item: {0} {1} {2}", _newItem.speedMulti, _newItem.handleMulti, _newItem.EnergieMulti));
		Debug.Log (string.Format ("Current item: {0} {1} {2}", currentItem.speedMulti, currentItem.handleMulti, currentItem.EnergieMulti));
		Debug.Log (string.Format ("Graph values: {0} {1} {2}", graphValues[0], graphValues[1], graphValues[2]));

		DOTween.To (x => _speedWidget.value = x, 0, graphValues [0], 0.2f);
		DOTween.To (x => _handlingWidget.value = x, 0, graphValues [1], 0.2f);
		DOTween.To (x => _energyWidget.value = x, 0, graphValues [2], 0.2f);
	}

	private void HandleInputs() {
		if (Input.GetAxis ("HorizontalButton") == -1) {
			PickHeldItem();
		}

		if (Input.GetAxis ("HorizontalButton") == 1) {
			PickNewItem();
		}

		if (Input.GetButtonDown("Fire1")) {
			StatManager.Instance.OnItemPick(_newItemIsPicked);
		}
	}

	private void PickHeldItem() {
		_newItemIsPicked = false;

		Vector3 heldPos = _heldItemPanel.rectTransform.anchoredPosition3D;
		Vector3 newPos = _newItemPanel.rectTransform.anchoredPosition3D;

		_heldItemPanel.DOColor (selectedColor, pickAnimDuration);
		_heldItemPanel.rectTransform.DOAnchorPos3D (new Vector3(heldPos.x, heldPos.y, -selectedZ), pickAnimDuration);

		_newItemPanel.DOColor (normalColor, pickAnimDuration);
		_newItemPanel.rectTransform.DOAnchorPos3D (new Vector3(newPos.x, newPos.y, 0), pickAnimDuration);
	}

	private void PickNewItem() {
		_newItemIsPicked = true;

		Vector3 heldPos = _heldItemPanel.rectTransform.anchoredPosition3D;
		Vector3 newPos = _newItemPanel.rectTransform.anchoredPosition3D;

		_heldItemPanel.DOColor (normalColor, pickAnimDuration);
		_heldItemPanel.rectTransform.DOAnchorPos3D (new Vector3(heldPos.x, heldPos.y, 0), pickAnimDuration);

		_newItemPanel.DOColor (selectedColor, pickAnimDuration);
		_newItemPanel.rectTransform.DOAnchorPos3D (new Vector3(newPos.x, newPos.y, -selectedZ), pickAnimDuration);
	}
}
