using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static UIManager Instance { get; private set; }

	public StatWidget speedWidget;
	public StatWidget handlingWidget;
	public StatWidget energyWidget;

	public void Awake() {
		Instance = this;
	}
}
