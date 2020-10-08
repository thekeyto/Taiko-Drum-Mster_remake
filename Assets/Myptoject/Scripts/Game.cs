using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    public sealed class N
    {
        public N(int t, double a, GameObject n) { type = t; atime = a; noteobj = n; }
        public int type;
        public double atime;
        public GameObject noteobj;
    }

    public NotePrefab tempnote;
    public GameObject musicsheet;
    public GameObject desFat;
    public GameObject lane;
    public GameObject startPoint;
    public GameObject musicobj;
    public GameObject checkpic;
    public SheetWrapper data;
    public Text time;
    public Text gradeText;
    public Text comboText;
    public scoreSystem score;
    private bool is_playing = false;

    private double timer = 0f;
    private int noteindex = 0;

    private int perfect = 0, great = 0, good = 0, maxcombo = 0;
    public AudioSource music;

    public List<N> taplist;
    public List<N> notActive;

    public float offset = 0f;

    private float lanes_ypos;
    private double dectime;
    private int grade = 0;
    private int flag = 0;
    private int combo = 0;
    private int keyid;
    private int activel,activer;
    private Vector3 spwan_point;
    private bool iskick;
    private void Awake()
    {
        musicobj = GameObject.Find("Music");
        data = GameObject.Find("SheetWapper").GetComponent<SheetWrapper>();

        spwan_point = startPoint.transform.position;

        music = musicobj.GetComponent<AudioSource>();
        music.clip = data.musicclip;
        music.Play();

        combo = 0;

        is_playing = true;
        iskick = false;
        keyid = -1;
        dectime = AudioSettings.dspTime;

        flag = 0;activel = 0;activer = 0;

        taplist = new List<N>();
        lanes_ypos = lane.transform.localPosition.y;
        foreach (var n in data.data.data)
        {
            taplist.Add(new N(n.type, n.atime, null));
        }
    }
    int keycodeid()//按键识别
    {/*
        if ((Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.K))) return 2;
        else if (Input.GetKeyDown(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) return 3;
        else*/ 
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.K)) return 0;
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J)) return 1;
        return -1;
    }
    void update()
    {
        score.perfect = perfect;
        score.great = great;
        score.good = good;
        score.maxcombo = maxcombo;
        score.grade = grade;
    }
    private void FixedUpdate()
    {
        if (timer >= data.data.summary.endtime)
        {
            is_playing = false;
            music.Stop();
            score.perfect = perfect;
            score.great = great;
            score.good = good;
            score.maxcombo = maxcombo;
            score.grade = grade;
            SceneManager.LoadScene("score");
        }
        if (is_playing)
        {
            update();
            gradeText.text = "Score:" + grade.ToString();
            comboText.text = "Combo:" + combo.ToString();
            time.text = timer.ToString();
            var deltatime = Time.deltaTime;
            timer = AudioSettings.dspTime-dectime;
            musicsheet.transform.localPosition -= new Vector3((float)G.CRAF.NOTES_SPEED * deltatime, 0f, 0f);
            desFat.transform.localPosition += new Vector3((float)G.CRAF.NOTES_SPEED * deltatime, (float)G.CRAF.NOTES_SPEED * deltatime, 0f);
            //生成音符
            if (flag!=taplist.Count())
            while (flag < taplist.Count() - 1 && taplist[flag].atime <= timer + 3)
            {
                var instnote = Instantiate(tempnote.itemlist[taplist[flag].type]);
                instnote.transform.SetParent(musicsheet.transform);
                instnote.transform.position = spwan_point + new Vector3((float)(taplist[flag].atime - timer ) * (float)G.CRAF.NOTES_SPEED, 0f, 0f);
                taplist[flag].noteobj = instnote;
                Destroy(instnote, (float)(taplist[flag].atime - timer + G.MISS_MARGIN));
                flag++;     
            }
            if (noteindex != taplist.Count())
            while (taplist[noteindex+1].atime - G.MISS_MARGIN <= timer && noteindex +1 <= taplist.Count() - 1)
            {
                maxcombo = Math.Max(maxcombo, combo);
                if (iskick == false) combo = 0;
                noteindex++;
                    iskick = false; keyid = -1;
                //Debug.Log(flag.ToString());
                //Debug.Log( flag.ToString()+' '+noteindex.ToString() + ' ' + taplist[noteindex].atime.ToString()+' '+timer.ToString());
                if (noteindex == taplist.Count()) break;
            }

            if (Math.Abs(taplist[noteindex].atime - timer)<=G.MISS_MARGIN && iskick == false)
            {
                if (keyid == -1|| keyid != taplist[noteindex].type)
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.K)) keyid = 0;
                    if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.J)) keyid = 1;
                    if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.K)) keyid = 2;
                    if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J)) keyid = 3;
                }
                Debug.Log(timer.ToString()+' '+taplist[noteindex].atime.ToString() + ' ' + taplist[noteindex].type.ToString() + ' ' + keyid);
                if (keyid != -1 && keyid == taplist[noteindex].type)
                {                
                    iskick = true;
                    Debug.Log("note "+noteindex.ToString()+ taplist[noteindex].noteobj.GetComponent<noteSprite>().isactive.ToString());
                    taplist[noteindex].noteobj.GetComponent<noteSprite>().isactive = true;
                    grade += getgrade(noteindex);
                }
            }
        }
    }
    int getgrade(int order)
    {
        double sub = Math.Abs(timer-taplist[order].atime);
        if (sub <= G.PERFECT_MARGIN)
        {
            combo++;perfect++;
            checkpic.GetComponent<checkSprite>().type = 0;
            return combo + 100;
        }
        else if (sub <= G.Great_MARGIN)
        {
            combo++;great++;
            checkpic.GetComponent<checkSprite>().type = 1;
            return combo + 50;
        }
        else if (sub <= G.GOOD_MARGIN)
        {
            combo++;good++;
            checkpic.GetComponent<checkSprite>().type = 2;
            return combo + 20;
        }
        else return 0;
    }
    public void BackMenu()
    {
        Destroy(GameObject.Find("SheetWapper"));
        SceneManager.LoadScene("Menu");
    }

    public void Getscore()
    {
        Destroy(GameObject.Find("SheetWapper"));
        SceneManager.LoadScene("score");
    }
}
