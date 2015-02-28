using System.Collections;

public class Stat {

	private StatWidget _widget;

    private int _level;
    private int _xp;
    private int _XpToLevel;
    private float _multi;

    public Stat(float multi, StatWidget widget)
    {
		_widget = widget;
		
		Multiplier = multi;
		XpToLevel = 10;
		XP = 0;
		Level = 1;
    }

    public void addXP(int value)
    {
		XP += (int)(value * Multiplier);
		while (XP >= XpToLevel) {
			XP -= XpToLevel;
			Level++;
		}
	}

    public int Level
    {
        get { return this._level; } 
		private set {
			_level = value;
			_widget.level = value;
		}
    }

    public int XP {
        get { return this._xp; }
		private set {
			this._xp = value;
			_widget.setXP (value, this._XpToLevel);
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

    public int XpToLevel
    { 
        get { return this._XpToLevel; } 
        set { this._XpToLevel = value; } 
    } 
}
