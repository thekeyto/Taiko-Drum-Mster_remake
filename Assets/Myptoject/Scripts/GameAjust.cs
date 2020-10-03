using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAjust : MonoBehaviour
{
    public sealed class N
    {
        public N(float a, int l, GameObject n) { atime = a; lane = l; noteobj = n; }
        public float atime;
        public int lane;
        public GameObject noteobj;
    }

    public GameObject tempnote;
    public GameObject musicsheet;
    public GameObject[] lanes;
    public MessageBox msgbox;
    public InputBox inbox;
    public SelectionControl sc;
    public Group_Editing ge;
    public NowLoading loading;
    public InputField author, title, difficulty, aside;
    public Button previewbtn, savebtn, autocompletebtn;
    
}
