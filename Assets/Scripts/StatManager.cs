using UnityEngine;
using System.Collections;
public class StatManager : MonoBehaviour {

    public static StatManager Instance { get; private set; }

    private Stat _speed;
    private Stat _handling;
    private Stat _energy;
    private ItemStats _item;

    private bool quickMode = true;

	void Awake() {
		Instance = this;
	}

	void Start () {
        _speed = new Stat(1, UIManager.Instance.speedWidget);
        _handling = new Stat(1, UIManager.Instance.handlingWidget);
        _energy = new Stat(2, UIManager.Instance.energyWidget);
        _item = new ItemStats();
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

        ItemStats tempItem=new ItemStats();
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
                            _speed.Multiplier = Mathf.Max(1 + Mathf.Round(value), _speed.Multiplier);
                        }
                        else
                        {
                            tempItem.speedMulti = 1 + Mathf.Round(value); 
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
                            _handling.Multiplier = Mathf.Max(1 + Mathf.Round(value), _handling.Multiplier);
                        }
                        else
                        {
                            tempItem.handleMulti = 1 + Mathf.Round(value);
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
                            _energy.Multiplier = Mathf.Max(1 + Mathf.Round(value), _energy.Multiplier);
                        }
                        else
                        {
                            tempItem.EnergieMulti = 1 + Mathf.Round(value);
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
                if (quickMode)
                {
                    _speed.Multiplier += pool;
                }
                else
                {
                    tempItem.speedMulti += pool;
                }
                break;
            case 2:

                if (quickMode)
                {
                    _handling.Multiplier += pool;
                }
                else
                {
                    tempItem.handleMulti += pool;
                }
                break;
            case 3:
                if (quickMode)
                {
                    _energy.Multiplier += pool;
                }
                else
                {
                    tempItem.EnergieMulti += pool;
                }
                break;
        }

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


}
