using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public class Interval
{
    public (double lower, double upper) bound;

    public void SetBoundary(double lower,double upper)
    {
        bound.lower = lower;
        bound.upper = upper;
    }

    public bool WhthinBound(double time)
    {
        return time >= bound.lower && time <= bound.upper;
    }
    public override string ToString()
    {
        return " Bound: [" + bound.lower.ToString() + "," + bound.upper.ToString() + "]";
    }
}