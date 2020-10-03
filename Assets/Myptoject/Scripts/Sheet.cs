using System;
using System.IO;

using System.Collections.Generic;
using UnityEngine;
using System.Text;

[Serializable]
public class sheet //音符的容器
{
    public sheetSummary summary;

    public List<Note> data = new List<Note>();
    public sheet()
    {
        summary = new sheetSummary 
        {
            version = G.VERSION,
            createdate = DateTime.Now.ToLongDateString()
        };
    }
    public void Random_Init(int total_notes)
    {
        for(int i=0;i<total_notes;i++)
        {
            Note n = new Note
            {
                ID = i,
                atime = (double)G.rng.Next(0, 60000),
                lane = G.rng.Next(0, G.LANES)
            };
            data.Add(n);
        }
    }
    public void Interpret(double startline = 0f, double activeline = 0f)
    {
        data.Sort((a, b) => { if (a.atime > b.atime) return -1; else return 1; });
        
        foreach(var d in data)
        {
            d.stime = d.atime - G.DISTANSE_SA;
            d.interval = new Interval();
            d.interval.SetBoundary(d.atime - G.MISS_MARGIN, d.atime + G.MISS_MARGIN);
            
            d.deadline = d.interval.bound.upper;
        }
    }
    public void Printout()
    {
        foreach(var d in data)
        {
            Debug.Log(d.ToString());
        }
    }
    public sheetSummary GetSummary()
    {
        return summary;
    }
}
[Serializable]
public class sheetSummary
{
    public string title = "";
    public string author = "";
    public int difficulty = 0;
    public string version = "";
    public string aside = "";
    public string createdate = "";
    public double endtime = 0f;
}
