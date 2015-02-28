using UnityEngine;
using System.Collections;
public class StatManager : MonoBehaviour {
	
    private Stat speed;
    private Stat handling;
    private Stat energy;

	void Start () {
        speed = new Stat(1, UIManager.Instance.speedWidget);
        handling = new Stat(1, UIManager.Instance.handlingWidget);
        energy = new Stat(2, UIManager.Instance.energyWidget);
	}

	void Update () {
		HandleDebugKeys ();        
	}

	void HandleDebugKeys() {
		if (Input.GetKeyDown(KeyCode.F))
		{
			speed.addXP(9);
			Debug.Log("Speed: LV:" + speed.Level + " XP:" + speed.XP);
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			handling.addXP(9);
			Debug.Log("Handle: LV:" + handling.Level + " XP:" + handling.XP);
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			energy.addXP(9);
			Debug.Log("Energy: LV:" + energy.Level + " XP:" + energy.XP);
		}
	}
}
