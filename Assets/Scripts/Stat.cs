using System.Collections;

public class Stat {

    private int _level = 1;
    private int _xp = 0;
    private int _XpToLevel=10;
    private float _multi = 1;
    public Stat(float multi)
    {
        _multi = multi;
    }

    public void XpAdd(int value)
    {
        _xp += (int)(value*_multi);
        while(_xp>=XpToLevel)
        {
            _xp -= XpToLevel;
            _level++;
        }
    }

    public int GetLevel
    {
        get { return this._level; } 
    }

    public int GetXp
    {
        get { return this._xp; } 
    }

    public float Multiplicator
    {
        get { return this._multi; }
        set { this._multi = value; }
    } 

    public int XpToLevel 
    { 
        get { return this._XpToLevel; } 
        set { this._XpToLevel = value; } 
    } 



}
