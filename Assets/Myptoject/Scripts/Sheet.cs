using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class sheet //音符的容器
{
    public sheetSummary summary;

    public List<Note>
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
    public string difficutty = "";
    public string version = "";
    public string aside = "";
    public string createdata = "";
    public float endtime = 0f;
}
