using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public NotePrefab tempnote;
    public GameObject musicsheet;
    public GameObject lane;
    public InputField title, difficulty;
    public Button previewbtn, savebtn;
    public Text time;
    private bool is_previewing = false;

    private double timer = 0f;
    private int noteindex = 0;

    public AudioSource music;
    public AudioSource[] se;

    public List<N> taplist;
    public List<N> selected;

    public float offset = 0f;

    private float lanes_ypos;
    private double dectime;
    private int flag = 0;

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
        Array.ForEach(se, x => x.volume = G.setting.sevolume);
    }
    private void Update()
    {
        if (is_previewing)
        {
            time.text = timer.ToString();
            var deltatime = Time.deltaTime;
            timer = AudioSettings.dspTime-dectime;
            musicsheet.transform.localPosition -= new Vector3((float)G.CRAF.NOTES_SPEED * deltatime, 0f,  0f);

            while (taplist[flag].atime <= timer + 10 && flag < taplist.Count())
            { 
                flag++; 
                var instnote = Instantiate(tempnote.itemlist[taplist[flag].type]);
                instnote.transform.SetParent(musicsheet.transform);
                instnote.transform.localPosition = new Vector3((float)(taplist[flag].atime-timer)*(float)G.CRAF.NOTES_SPEED, lanes_ypos,  0f);
                taplist[flag].noteobj = instnote;
            }
            while(taplist[noteindex].atime<=timer)
            {
                se[taplist[noteindex].type].Play();
                Destroy(taplist[noteindex].noteobj);
                noteindex++;
                if (noteindex>taplist.Count)
                {
                    is_previewing = false; previewbtn.interactable = true; music.Stop();
                    break;
                }
            }
            Debug.Log(noteindex + ' ' + taplist[noteindex].atime);
        }
    }
    public void ResetNote(N note)
    {
        note.noteobj.transform.localPosition = new Vector3((float)lanes_ypos, (float)(activeline + note.atime * G.CRAF.NOTES_SPEED), 0f);
    }
    public void TransferAndSave()
    {
        if (taplist.Count == 0)
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
                type = n.type,
                ID = id,
                lane = n.lane
            };
            sheet.data.Add(newn);
            id += 1;
        }
        sheet.summary.title = title.text;
        sheet.summary.difficulty = int.Parse(difficulty.text);
        sheet.summary.endtime = taplist[taplist.Count - 1].atime + 2f;

        // 保存乐谱
        filesystem.Save_MusicSheet(sheet, title.text);
    }
    
    public void PreviewSheet()
    {
        if (!is_previewing)
        {
            previewbtn.interactable = false;
            timer = 0;
            music.time = 0;
            noteindex = 0;
            music.Play();
            dectime = AudioSettings.dspTime;
            is_previewing = true;
        }
    }
    public void SortN()
    {
        taplist.Sort(
            (a, b) => { if (a.atime > b.atime) { return 1; } else { return -1; } });
    }
    public void BackMenu()
    {
        Destroy(music);
        SceneManager.LoadScene("Menu");
    }
}
