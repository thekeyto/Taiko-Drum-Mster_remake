using System;
using UnityEngine;

[Serializable]
public class Setting
{
    public int language = 0;
    public float musicvolume = 1f;
    public float sevolume = 1f;
    public (int, int) resolution = (1920, 1080);
    public KeyCode[] lanekey = new KeyCode[7] { KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L };
}
