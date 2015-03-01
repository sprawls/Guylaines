﻿using UnityEngine;
using System.Collections;
public class StatManager : MonoBehaviour {

    public static StatManager Instance { get; private set; }

	private int _runsRemaining;
	private float _toNextRun;
	private float _distance;
	private int _runsCollected = 0;

    private Stat _speed;
    private Stat _handling;
    private Stat _energy;
    private ItemStats _item;
	private ItemStats _tempItem;

    private ItemHolder holder;
    public ShipControl controler;
    public PlaySound soundPlayer;

    private bool quickMode = false;

	void Awake() {
		Instance = this;
	}

	void Start () {
        controler = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<ItemHolder>();
        soundPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlaySound>();

        loadItem();
        _speed = new Stat(_item.speedMulti, UIManager.Instance.speedWidget);
        _handling = new Stat(_item.handleMulti, UIManager.Instance.handlingWidget);
        _energy = new Stat(_item.EnergieMulti, UIManager.Instance.energyWidget);

		_tempItem = new ItemStats (1, 1, 1);

		RunsRemaining = 3;
		ToNextRun = 50 + 10*Mathf.Pow (_runsCollected, 2);
		Distance = 0;
	}

	void Update () {
		HandleDebugKeys();
		UpdateDistance ();
	}

    public void genererItem(float pool)
    {
		bool speedPicked = false;
		bool hendlePicked = false;
		bool energiePicked = false;

		for (int i = 0; i < 3; i++) {
			float value = Random.Range (0.0f, pool);
			switch (Random.Range (1, 4)) {
			case 1:
				if (!speedPicked) {
					speedPicked = true;
					if (quickMode) {
						_tempItem.speedMulti += Mathf.Max (Mathf.Round (value), _speed.Multiplier);
					} else {
						_tempItem.speedMulti += Mathf.Round (value); 
					}
					pool -= Mathf.Round (value);
				} else {
					i--;
				}
				break;
			case 2:
				if (!hendlePicked) {
					hendlePicked = true;
					if (quickMode) {
						_tempItem.handleMulti += Mathf.Max (Mathf.Round (value), _handling.Multiplier);
					} else {
						_tempItem.handleMulti += Mathf.Round (value);
					}
					pool -= Mathf.Round (value);
				} else {
					i--;
				}
				break;
			case 3:
				if (!energiePicked) {
					energiePicked = true;
					if (quickMode) {
						_tempItem.EnergieMulti += Mathf.Max (Mathf.Round (value), _energy.Multiplier);
					} else {
						_tempItem.EnergieMulti += Mathf.Round (value);
					}
					pool -= Mathf.Round (value);
				} else {
					i--;
				}
				break;
			}
            
		}
		pool = Mathf.Round (pool);
		switch (Random.Range (1, 4)) {
		case 1:
			_tempItem.speedMulti += pool;
			break;
		case 2:
			_tempItem.handleMulti += pool;
			break;
		case 3:
			_tempItem.EnergieMulti += pool;
			break;
		}
        soundPlayer.playSlowMo();
		if (!quickMode) {
			ItemUIBehaviour.Instance.OpenUI (_tempItem);
            controler.StartBullteTime(2.0f);
		} else {
			saveItem(_tempItem);
		}
	}

	public void OnItemPick(bool newItemWasPicked) {
        controler.callStopBullteTime(0.0f);
		if(newItemWasPicked)
        {
            saveItem(_tempItem);
            _tempItem = new ItemStats(1, 1, 1);
        }
		ItemUIBehaviour.Instance.CloseUI ();
    }

    private void saveItem(ItemStats item)
    {

        holder.item = item;
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

	public void eraseItem()
	{
		holder.item = new ItemStats(1, 1, 1);
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

		if (Input.GetKeyDown (KeyCode.T)) {
			genererItem (5);
		}
	}

	private void UpdateDistance() {
		Distance = transform.position.z;
		BestRun = Mathf.Max (BestRun, Distance);
		////Debug.Log (Distance);
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
	
	public int RunsRemaining {
		get { return _runsRemaining; }
		private set {
			_runsRemaining = value;
			ScoreUIBehaviour.Instance.RunsRemaining = _runsRemaining + (1 - _toNextRun/(50 + 10*Mathf.Pow (_runsCollected, 2)));
		}
	}
	
	public float ToNextRun {
		get { return _toNextRun; }
		private set {
			_toNextRun = value;
			ScoreUIBehaviour.Instance.ToNextRun = _toNextRun;
		}
	}
	
	public float Distance {
		get { return _distance; }
		private set {
			_distance = Mathf.Max (0, value);
			ScoreUIBehaviour.Instance.Distance = _distance;
		}
	}
	
	public float BestRun {
		get { return holder._bestRun; }
		private set {
			holder._bestRun = value;
			ScoreUIBehaviour.Instance.Best = holder._bestRun;
		}
	}
}
