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
    private bool is_playing = false;

    private double timer = 0f;
    private int noteindex = 0;

    public AudioSource music;

    public List<N> taplist;
    public List<N> notActive;

    public float offset = 0f;

    private float lanes_ypos;
    private double dectime;
    private int grade = 0;
    private int flag = 0;
    private int combo = 0;
    private int activel,activer;
    private Vector3 spwan_point;
    private void Awake()
    {
        musicobj = GameObject.Find("Music");
        data = GameObject.Find("SheetWapper").GetComponent<SheetWrapper>();

        spwan_point = startPoint.transform.position;

        music = musicobj.GetComponent<AudioSource>();
        music.clip = data.musicclip;

        combo = 0;

        is_playing = true;
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
    {
        if ((Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.K))) return 2;
        else if (Input.GetKeyDown(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) return 3;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.K)) return 0;
        else if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J)) return 1;
        return -1;
    }
    private void Update()
    {
        if (is_playing)
        {
            Debug.Log(noteindex+" "+taplist[noteindex].atime );
            gradeText.text = "Score:" + grade.ToString();
            comboText.text = "Combo:" + combo.ToString();
            time.text = timer.ToString();
            var deltatime = Time.deltaTime;
            timer += Time.deltaTime;
            musicsheet.transform.localPosition -= new Vector3((float)G.CRAF.NOTES_SPEED * deltatime, 0f, 0f);
            desFat.transform.localPosition += new Vector3((float)G.CRAF.NOTES_SPEED *  deltatime, (float)G.CRAF.NOTES_SPEED * deltatime, 0f);
            //生成音符
            while (taplist[flag].atime <= timer + 2 && flag < taplist.Count - 1)
            {
                flag++;
                var instnote = Instantiate(tempnote.itemlist[taplist[flag].type]);
                instnote.transform.SetParent(musicsheet.transform);
                instnote.transform.position = spwan_point+ new Vector3( (float)(taplist[flag].atime - timer) * (float)G.CRAF.NOTES_SPEED, 0f, 0f);
                taplist[flag].noteobj = instnote;
                Destroy(instnote, (float)(taplist[flag].atime - timer + G.MISS_MARGIN));
            }

            while (taplist[noteindex].atime - G.MISS_MARGIN <= timer&&noteindex<taplist.Count()-1)
                noteindex++;
            if (taplist[noteindex].atime-G.MISS_MARGIN<=timer)
            {
                int keyid = keycodeid();
                if (keyid != -1 && keyid == taplist[noteindex].type)
                {
                    taplist[noteindex].noteobj.GetComponent<noteSprite>().desFat = desFat;
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
            combo ++;
            checkpic.GetComponent<checkSprite>().type = 0;
            return combo+100;
        }
        else if (sub <= G.Great_MARGIN)
        {
            combo++;

            checkpic.GetComponent<checkSprite>().type = 1;
            return combo + 50;
        }
        else if (sub <= G.GOOD_MARGIN)
        {
            combo ++;
            checkpic.GetComponent<checkSprite>().type = 2;
            return combo+20;
        }
        else
        {
            combo = 0;
            return 0;
        }
    }
    public void BackMenu()
    {
        Destroy(GameObject.Find("SheetWapper"));
        SceneManager.LoadScene("Menu");
    }
}
