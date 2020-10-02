using System;
using UnityEngine;
[Serializable]
public class Note//音符的类
{
    public int ID;
    public int type;
    public int lane;
    public double atime;
    [NonSerialized] public double stime = 0;
    [NonSerialized] public Interval interval;
    [NonSerialized] public double deadline = 0f;
    [NonSerialized] public GameObject noteobj = null;
    public override string ToString()
    {
        return "ID: " + ID.ToString() + " Lane: " + lane.ToString() + " Active time: " + atime.ToString() + " Spawn time: " + stime.ToString() + " Deadline: " + deadline.ToString() + " Intervals " + interval.ToString();
    }
}