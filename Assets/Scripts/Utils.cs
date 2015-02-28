using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Utils
{
    public static float Range(this Random rnd, float min, float max)
    {
        double d = rnd.NextDouble();
        double mind = min;
        double maxd = max;

        float res = (float)(d * (maxd - mind) + mind);
        return res;
    }

    public static T OneAtRandom<T>(this List<T> lst, Random rnd)
    {
        if(rnd == null)
        {
            rnd = new Random();
        }
        return lst[rnd.Next(lst.Count)];
    }
}

