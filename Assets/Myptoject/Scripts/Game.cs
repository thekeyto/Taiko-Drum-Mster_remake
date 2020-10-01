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

    public float timer = 0;

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
    
    public float sline,aline;

    public GameObject activeline, spawnline;
    private Note tracknote;

    public bool usekeycode = true;
    private KeyCode[] lanekeycodes;

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
        stock.
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
