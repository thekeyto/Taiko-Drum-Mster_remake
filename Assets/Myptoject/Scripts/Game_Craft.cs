using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Game_Craft : MonoBehaviour//完成谱面的制作
{
    public (string musicpath, string musictype) path;
    public double timer = 0f;
    public Text time;
    public Button startbtn, finishbtn;
    public bool timerswitch = false;
    public AudioSource[] drum = new AudioSource[4];

    public AudioSource music;
    public DataWrapper dwapper;
    public GameObject notes;
    public NotePrefab notePrefab;
    public GameObject[] tempnote=new GameObject[5];
    public GameObject startingnote;

    public Vector3 anchor_spawn;
    
    private double dectime=0;
    private void Awake()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();

        dwapper.taplist = new List<(int,double)>();
        anchor_spawn = startingnote.transform.position;
    }

    private void Update()
    {
        if (timerswitch)
        {
            timer = AudioSettings.dspTime-dectime;
            time.text = timer.ToString();
            inputkeycode();
            notes.transform.localPosition -= new Vector3((float)G.CRAF.NOTES_SPEED * Time.deltaTime, 0f, 0f);
            Debug.Log(G.NOTE_SPEED.ToString());
        }
    }

    int keycodeid()//按键识别
    {
        if ((Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.K))) return 2;
        else if (Input.GetKeyDown(KeyCode.F) && Input.GetKeyDown(KeyCode.J)) return 3;
        else 
        if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.K)) return 0;
        else if (Input.GetKeyDown(KeyCode.F)|| Input.GetKeyDown(KeyCode.J)) return 1;
        return -1;
    }
    public void inputkeycode()
    {
        int keyId = keycodeid();
        if (keyId!=-1)
        {
            drum[keyId].Play();
            dwapper.taplist.Add((keyId,timer));
            Debug.Log("Time " + timer.ToString() + " added to the list");

            StartCoroutine(CreateNote(keyId));
        }
    }
    public void startRecording()
    {
        music.Play();
        finishbtn.interactable = true;
        startbtn.interactable = false;
        timerswitch = true;
        dectime = AudioSettings.dspTime;
    }
    public void stopRecording()
    {
        timerswitch = false;
        music.Stop();
        SceneManager.LoadScene("gameAdjust");

    }
    public IEnumerator CreateNote(int id)
    {
        var note = Instantiate(notePrefab.itemlist[id]);
        note.transform.SetParent(notes.transform);
        note.transform.position = anchor_spawn;
        Destroy(note, 4.0f);
        yield return null;
    }
}