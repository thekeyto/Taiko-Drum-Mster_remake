using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Game_Craft : MonoBehaviour
{
    public (string musicpath, string musictype) path;
    public double timer = 0f;
    public Text time;
    public Button startbtn, finishbtn, tapbtn;
    public bool timerswitch = false;

    public AudioSource music;
    public DataWrapper dwapper;
    public GameObject notes;
    public GameObject tempnote;
    public GameObject startingnote;

    public Vector3 anchor_spawn;

    private void Awake()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();

        dwapper.taplist = new List<double>();
        anchor_spawn = startingnote.transform.position;
    }

    private void Update()
    {
        if (timerswitch)
        {
            time.text = timer.ToString();
            timer += Time.deltaTime;

            notes.transform.localPosition -= new Vector3((float)G.NOTE_SPEED * Time.deltaTime, 0f, 0f);
        }
    }

    public void Tap()
    {
        dwapper.taplist.Add(timer);
        Debug.Log("Time " + timer.ToString() + " added to the list");

        StartCoroutine(CreateNote());
    }

    public IEnumerator CreateNote()
    {
        var note = Instantiate(tempnote);
        note.transform.SetParent(notes.transform);
        note.transform.position = anchor_spawn;
        yield return null;
    }
}
