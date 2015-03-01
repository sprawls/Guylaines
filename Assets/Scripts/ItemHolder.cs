using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour {

    private ItemStats _item= new ItemStats(1,1,1);

	public int _runsRemaining = 3;
	public float _runsCollected = 0;
	public float _toNextRun;

	public float _collectedEnergy = 0;
	public float _energyNeeded = 40;

	public float _bestRun = 0;

    public ItemStats item
    {
        get { return this._item; }
        set { this._item = value; }
    }

	void Awake() {
        if (GameObject.FindGameObjectWithTag("Holder") == null)
        {
            gameObject.tag = "Holder";
            DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}