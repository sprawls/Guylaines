using System.Collections;

public class Stat {
	public const int XP_TO_LEVEL = 100;
	public int[] xpNeeded = {100,500,1300,2500,4100,6100,8500,11300,14500,18100,22100};

	private StatWidget _widget;

    private int _level;
	private int _prevLevel;
    private int _xp;
    private float _multi;

    public Stat(float multi, StatWidget widget)
    {
		_widget = widget;

		Multiplier = multi;
		XP = 0;
		Level = 1;
    }

    public int addXP(float value) {
		int prevLevel = _level;

		XP += (int)(value * Multiplier);
        if (Level < 9999)
        {
			if(xpNeeded.Length > Level-1) {
				//Level = 1 + XP / xpNeeded[_level-1];
				Level = 1 + XP / XP_TO_LEVEL;
			} else {
				Level = 1 + XP / 10000000;
			}
        }
        else
        {
            Level = 9999;
        }

		return _level - prevLevel;
	}

    public int Level {
        get { return this._level; } 
		private set {
			_level = value;
			if (_prevLevel != _level) {
				_widget.level = value;
			}
			_prevLevel = _level;
		}
    }

    public int XP {
        get { return this._xp; }
		private set {
			this._xp = value;
			_widget.setXP (value);
		}
    }

    public float Multiplier
    {
        get { return this._multi; }
        set {
			this._multi = value;
			_widget.multiplier = value;
		}
    }
}
