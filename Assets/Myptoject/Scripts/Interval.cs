using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public class Interval
{
    public (float lower, float upper) bound;

    public void SetBoundary(float lower,float upper)
    {
        bound.lower = lower;
        bound.upper = upper;
    }

    public bool WhthinBound(float time)
    {
        return time >= bound.lower && time <= bound.upper;
    }
    public override string ToString()
    {
        return " Bound: [" + bound.lower.ToString() + "," + bound.upper.ToString() + "]";
    }
}