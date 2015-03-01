using System.Collections;

public class Stat {
	public const int XP_TO_LEVEL = 100;

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

    public void addXP(float value)
    {
		XP += (int)(value * Multiplier);
		Level = 1 + XP / XP_TO_LEVEL;
	}

    public int Level
    {
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
