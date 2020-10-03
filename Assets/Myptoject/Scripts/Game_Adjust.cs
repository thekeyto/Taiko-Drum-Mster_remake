using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Game_Adjust : MonoBehaviour
{
    public sealed class N
    {
        public N(int t,double a, int l, GameObject n) { type = t; atime = a;lane = l; noteobj = n; }
        public int type;
        public double atime;
        public int lane;
        public GameObject noteobj;
    }

    public GameObject tempnote;
    public GameObject musicsheet;
    public GameObject lane;
    public MessageBox msgbox;
    public InputBox inbox;
    public Group_Editing ge;
    public SelectionControl sc;
    public NowLoading loading;
    public InputField author, title, difficulty, aside;
    public Button previewbtn, savebtn, autocompletebtn;
    private bool is_previewing = false;

    private double timer = 0f;
    private int noteindex = 0;

    public AudioSource music;
    public AudioSource[] se;

    public List<N> taplist;
    public List<N> selected;

    public float offset = 0f;

    private float lanes_ypos;

    private readonly float activeline = -500.0f;

    private readonly Vector3 sheetshift = new Vector3(0f, 5f, 0f);

    private void Awake()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        var obj = GameObject.Find("Data");

        selected = new List<N>(15);
        taplist = new List<N>(100);
        lanes_ypos = lane.transform.localPosition.x;
        foreach(var (a,b) in obj.GetComponent<DataWrapper>().taplist)
        {
            taplist.Add(new N(a, b, 2, null));
        }
        Destroy(obj);
        SetNotesOnScreen();
        Array.ForEach(se, x => x.volume = G.setting.sevolume);
    }
    private void Start()
    {
        msgbox.Show(G.lang.message_sheet_edit_guides[G.setting.language]);
    }
    private void Update()
    {
        if (is_previewing)
        {
            var deltatime = Time.deltaTime;
            timer = AudioSettings.dspTime;
            musicsheet.transform.localPosition -= new Vector3(0f, (float)G.CRAF.NOTES_SPEED * deltatime, 0f);
            while(taplist[noteindex].atime<=timer)
            {
                se[taplist[noteindex].lane].Play();
                noteindex++;
                if (noteindex>taplist.Count)
                {
                    is_previewing = false; previewbtn.interactable = true; music.Stop();
                    break;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                sc.DeSelect();
                if (offset >= 0f)
                {
                    musicsheet.transform.localPosition -= sheetshift;
                    offset += 5f;
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                sc.DeSelect();
                if (offset > 0f)
                {
                    musicsheet.transform.localPosition += sheetshift;
                    offset -= offset >= 5f ? 5f : offset;
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                ge.ShiftUp();
            }
            else if (Input.GetKey(KeyCode.F))
            {
                ge.ShiftDown();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ge.ShiftLeft();
            }
            else if (Input.GetKey(KeyCode.G))
            {
                ge.ShiftRight();
            }
        }
    }
    public void ResetNote(N note)
    {
        note.noteobj.transform.localPosition = new Vector3((float)lanes_ypos, (float)(activeline + note.atime * G.CRAF.NOTES_SPEED), 0f);
    }
    public void TransferAndSave()
    {
        // verify input
        if (author.text == "" || title.text == "" || difficulty.text == "")
        {
            msgbox.Show(G.lang.message_please_fill_required_fields[G.setting.language]);
            return;
        }
        else if (taplist.Count == 0)
        {
            return;
        }

        SortN();

        int id = 0;
        var sheet = new sheet();
        foreach (var n in taplist)
        {
            Note newn = new Note
            {
                atime = n.atime,
                ID = id,
                lane = n.lane
            };
            sheet.data.Add(newn);
            id += 1;
        }
        sheet.summary.author = author.text;
        sheet.summary.title = title.text;
        sheet.summary.difficulty = int.Parse(difficulty.text);
        sheet.summary.aside = aside.text;
        sheet.summary.endtime = taplist[taplist.Count - 1].atime + 2f;

        // Save the music sheet
        filesystem.Save_MusicSheet(sheet, title.text);
    }
    private void SetNotesOnScreen()
    {
        foreach (var n in taplist)
        {
            var instnote = Instantiate(tempnote);
            instnote.transform.SetParent(musicsheet.transform);
            instnote.transform.localPosition = new Vector3(lanes_ypos, (float)(activeline + n.atime * G.CRAF.NOTES_SPEED), 0f);
            n.noteobj = instnote;
        }
    }
    public void SortN()
    {
        taplist.Sort(
            (a, b) => { if (a.atime > b.atime) { return 1; } else { return -1; } }
            );
    }
}
