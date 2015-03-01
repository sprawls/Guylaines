using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour {

    private ItemStats _item= new ItemStats(1,1,1);
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