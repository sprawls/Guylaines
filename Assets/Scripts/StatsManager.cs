using UnityEngine;
using System.Collections;
public class StatsManager : MonoBehaviour {


    private Stat speed;
    private Stat handle;
    private Stat energy;


	// Use this for initialization
	void Start () {
        speed = new Stat(1);
        handle = new Stat(1);
        energy = new Stat(2);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            speed.XpAdd(9);
            Debug.Log("Speed: LV:" + speed.GetLevel + " XP:" + speed.GetXp);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            handle.XpAdd(9);
            Debug.Log("Handle: LV:" + handle.GetLevel + " XP:" + handle.GetXp);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            energy.XpAdd(9);
            Debug.Log("Energy: LV:" + energy.GetLevel + " XP:" + energy.GetXp);
        }
        
	}
}
