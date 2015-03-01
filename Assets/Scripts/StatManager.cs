using UnityEngine;
using System.Collections;
public class StatManager : MonoBehaviour {

    public static StatManager Instance { get; private set; }

    private Stat _speed;
    private Stat _handling;
    private Stat _energy;
    private ItemStats _item;

    private ItemHolder holder;

    private bool quickMode = true;

	void Awake() {
		Instance = this;
	}

	void Start () {
        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<ItemHolder>();
        loadItem();
        _speed = new Stat(_item.speedMulti, UIManager.Instance.speedWidget);
        _handling = new Stat(_item.handleMulti, UIManager.Instance.handlingWidget);
        _energy = new Stat(_item.EnergieMulti, UIManager.Instance.energyWidget);
	}

	void Update () {
		HandleDebugKeys();        
	}

	public Stat Speed {
		get { return _speed; }
	}

    public ItemStats ItemStat {
        get { return _item; }
    }

	public Stat Handling {
		get { return _handling; }
	}

	public Stat Energy {
		get { return _energy; }
	}



    public void choisirItem(float pool)
    {
        bool speedPicked = false;
        bool hendlePicked = false;
        bool energiePicked = false;

        ItemStats tempItem = new ItemStats(1,1,1);
        for (int i = 0; i < 3; i++)
        {
            float value = Random.Range(0.0f, pool);
            switch (Random.Range(1, 4))
            {
                case 1:
                    if (!speedPicked)
                    {
                        speedPicked = true;
                        if (quickMode)
                        {
                            tempItem.speedMulti += Mathf.Max(Mathf.Round(value), _speed.Multiplier);
                        }
                        else
                        {
                            tempItem.speedMulti +=  Mathf.Round(value); 
                        }
                        pool -= Mathf.Round(value);
                    }
                    else
                    {
                        i--;
                    }
                    break;
                case 2:
                    if (!hendlePicked)
                    {
                        hendlePicked = true;
                        if (quickMode)
                        {
                            tempItem.handleMulti  += Mathf.Max( Mathf.Round(value), _handling.Multiplier);
                        }
                        else
                        {
                            tempItem.handleMulti +=  Mathf.Round(value);
                        }

                        pool -= Mathf.Round(value);
                    }
                    else
                    {
                        i--;
                    }
                    break;
                case 3:
                    if (!energiePicked)
                    {
                        energiePicked = true;
                        if (quickMode)
                        {
                            tempItem.EnergieMulti += Mathf.Max( Mathf.Round(value), _energy.Multiplier);
                        }
                        else
                        {
                            tempItem.EnergieMulti +=  Mathf.Round(value);
                        }
                        pool -= Mathf.Round(value);
                    }
                    else
                    {
                        i--;
                    }
                    break;
            }
            
        }
        pool = Mathf.Round(pool);
        switch (Random.Range(1, 4))
        {
            case 1:
                    tempItem.speedMulti += pool;
                break;
            case 2:
                    tempItem.handleMulti += pool;
                break;
            case 3:
                    tempItem.EnergieMulti += pool;
                break;
        }

        if (!quickMode)
        {
            if(true/*envoyer tempItem a interface de choix qui retournetas trus si le nouveau est accepter*/)
            {
                saveItem(tempItem);
            }
        }
        else
        {
            Debug.Log(tempItem.speedMulti + " - " + tempItem.handleMulti + " - " + tempItem.EnergieMulti);

            saveItem(tempItem);
        }

    }

    private void saveItem(ItemStats item)
    {

        holder.item = item;
        Debug.Log(item.speedMulti+" - "+ item.handleMulti+" - "+ item.EnergieMulti);
        _item = item;
        applyItemStats();
    }

    private void applyItemStats()
    {
        _speed.Multiplier = _item.speedMulti;
        _handling.Multiplier = _item.handleMulti;
        _energy.Multiplier = _item.EnergieMulti;
    }

    private void loadItem()
    {
        _item = holder.item;
    }

	void HandleDebugKeys() {
		if (Input.GetKey(KeyCode.F))
		{
			_speed.addXP(98);
		}
		if (Input.GetKey(KeyCode.G))
		{
			_handling.addXP(98);
		}
		if (Input.GetKey(KeyCode.H))
		{
			_energy.addXP(98);
		}
	}


    public void eraseItem()
    {
        holder.item = new ItemStats(1, 1, 1);
    }


}
