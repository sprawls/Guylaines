using UnityEngine;
using System.Collections;
public class StatManager : MonoBehaviour {

	public static StatManager Instance { get; private set; }

    private Stat _speed;
    private Stat _handling;
    private Stat _energy;

	void Awake() {
		Instance = this;
	}

	void Start () {
        Debug.Log(UIManager.Instance);
        _speed = new Stat(1, UIManager.Instance.speedWidget);
        _handling = new Stat(1, UIManager.Instance.handlingWidget);
        _energy = new Stat(2, UIManager.Instance.energyWidget);
	}

	void Update () {
		HandleDebugKeys();        
	}

	public Stat Speed {
		get { return _speed; }
	}

	public Stat Handling {
		get { return _handling; }
	}

	public Stat Energy {
		get { return _energy; }
	}

	void HandleDebugKeys() {
		if (Input.GetKeyDown(KeyCode.F))
		{
			_speed.addXP(90);
			Debug.Log("Speed: LV:" + _speed.Level + " XP:" + _speed.XP);
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			_handling.addXP(90);
			Debug.Log("Handle: LV:" + _handling.Level + " XP:" + _handling.XP);
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			_energy.addXP(90);
			Debug.Log("Energy: LV:" + _energy.Level + " XP:" + _energy.XP);
		}
	}


}
