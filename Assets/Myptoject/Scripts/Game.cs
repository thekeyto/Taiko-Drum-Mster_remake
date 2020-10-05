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
    public GameObject lane;
    public GameObject musicobj;
    public SheetWrapper data;
    public Text time;
    private bool is_playing = false;

    private double timer = 0f;
    private int noteindex = 0;

    public AudioSource music;
    public AudioSource[] se;

    public List<N> taplist;

    public float offset = 0f;

    private float lanes_ypos;
    private double dectime;
    private int flag = 0;


    private void Awake()
    {
        musicobj = GameObject.Find("Music");
        data = GameObject.Find("SheetWapper").GetComponent<SheetWrapper>();
        
        music = musicobj.GetComponent<AudioSource>();
        music.clip = data.musicclip;

        is_playing = true;
        dectime = AudioSettings.dspTime;

        taplist = new List<N>();
        lanes_ypos = lane.transform.localPosition.y;
        foreach (var n in data.data.data)
        {
            taplist.Add(new N(n.type, n.atime, null));
        }
        Destroy(GameObject.Find("SheetWapper"));
    }
    private void Update()
    {
        Debug.Log(timer.ToString());
        if (is_playing)
        {
            time.text = timer.ToString();
            var deltatime = Time.deltaTime;
            timer = AudioSettings.dspTime - dectime;
            musicsheet.transform.localPosition -= new Vector3((float)G.CRAF.NOTES_SPEED * 100 * deltatime, 0f, 0f);

            while (taplist[flag].atime <= timer + 3 && flag < taplist.Count - 1)
            {
                flag++;
                var instnote = Instantiate(tempnote.itemlist[taplist[flag].type]);
                instnote.transform.SetParent(musicsheet.transform);
                instnote.transform.localPosition = new Vector3(-309.3256f + 100 * (float)(taplist[flag].atime - timer) * (float)G.CRAF.NOTES_SPEED, 193, 0f);
                taplist[flag].noteobj = instnote;
            }
            while (taplist[noteindex].atime <= timer)
            {
                Debug.Log(taplist[noteindex].type.ToString());
                se[taplist[noteindex].type].Play();

                Destroy(taplist[noteindex].noteobj);
                noteindex++;
                if (noteindex >= taplist.Count())
                {
                    is_playing = false;
                    break;
                }
            }
        }
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
