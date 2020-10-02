using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public sheet stock=new sheet();
    public Stack<Note> pending = new Stack<Note>();
    public TapArea[] lanes;
    public Queue<Note>[] active;
    public List<GameObject> note_obj;
    public bool[] hold;

    public double timer = 0;

    public bool timerswitch;
    public Text t_time;
    
    public int points;
    public Text t_points;

    public GameObject note_template;
    public GameObject board;

    public AudioSource music;
    public AudioSource[] se;

    public GameObject[] anchors;
    public Vector3[] anchor_pos = new Vector3[G.LANES];
    public Vector3[] anchor_rot = new Vector3[G.LANES];

    public int combo = 0;

    public bool is_play = false;
    
    public double sline,aline;

    public GameObject activeline, spawnline;
    private Note tracknote;

    public bool usekeycode = true;
    private KeyCode[] lanekeycodes;

    private double dsptimesong = 0.0f;
    private void Awake()
    {
        note_obj = new List<GameObject>(500);
        lanekeycodes = new KeyCode[G.LANES];

        timer = -G.DISTANSE_SA;

        active = new Queue<Note>[G.LANES];
        hold = new bool[1] { false };
        for(var i=0;i<G.LANES; i++)
        {
            active[i] = new Queue<Note>(15);

            anchor_pos[i] = anchors[i].transform.localPosition;
            anchor_rot[i] = anchors[i].transform.localEulerAngles;
        }

        var wrapper = GameObject.Find("SheetWrapper").GetComponent<SheetWrapper>();
        stock = wrapper.data;
        stock.Interpret(sline, aline);
        stock.Printout();
        music.clip = wrapper.musicclip;

        SetNotes();
    }
    private void Start()
    {
        dsptimesong = AudioSettings.dspTime;
    }
    void Update()
    {
        if (timerswitch)
        {
            t_time.text = timer.ToString();
            timer = AudioSettings.dspTime-dsptimesong;
            if (timer > 0 && !is_play) { music.Play();is_play = true; }

            double dtime = Time.deltaTime;
            foreach(var lane in active)
            {
                foreach(var anote in lane)
                {
                    anote.noteobj.transform.localPosition -= new Vector3((float)(G.NOTE_SPEED * dtime), 0.0f, 0.0f);
                }
            }
            Count();
            if (stock.summary.endtime <= timer)
            {
                timerswitch = false;
                if (music.isPlaying)
                    music.Stop();
            }
            for (int i = 0; i < G.LANES; i++)
            {
                if (Input.GetKeyDown(lanekeycodes[i]))
                {
                    lanes[i].PointerDown();
                }
                else
                if (Input.GetKeyUp(lanekeycodes[i]))
                {
                    lanes[i].PointerUp();
                }
            }
        }
    }
    private void SetNotes()
    {
        foreach(var n in stock.data)
        {
            var note = Instantiate(note_template);
            note.transform.SetParent(board.transform);
            note.transform.localPosition = anchor_pos[n.lane];
            note.transform.localEulerAngles = anchor_rot[n.lane];
            note.transform.localScale = new Vector3(0f, 0f, 0f);
            note.GetComponent<ParticleSystem>().Stop();
            n.noteobj = note;
            note_obj.Add(note);
        }
        tracknote = stock.data[stock.data.Count - 1];
    }
    private void Count()
    {
        foreach(var d in active)
        {
            if (d.Count!=0)
            {
                while (timer >= d.Peek().deadline)
                {
                    var top = d.Dequeue();
                    combo = 0;
                    Debug.Log("Note " + top.ID.ToString() + " deadline " + top.deadline.ToString() + "/" + timer.ToString() + " destroyed at " + top.noteobj.transform.localPosition.y);
                    Destroy(top.noteobj);
                    if (d.Count == 0) break;
                }
            }
        }

        if (stock.data.Count != 0)
        {
            while (stock.data[stock.data.Count - 1].stime <= timer)
            {
                Note topnote = stock.data[stock.data.Count - 1];
                topnote.noteobj.transform.localScale = new Vector3(1f, 1f, 1f);
                topnote.noteobj.GetComponent<ParticleSystem>().Play();
                active[topnote.lane].Enqueue(topnote);
                stock.data.RemoveAt(stock.data.Count - 1);
                Debug.Log(topnote.ID.ToString() + " poped");

                if (stock.data.Count == 0)
                {
                    break;
                }
            }
        }
    }
    public void start()
    {
        timerswitch = true;
    }
}
