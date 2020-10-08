using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MusicItem : MonoBehaviour
{
    public filesystem.FilePath path;
    public SheetWrapper wrapper;
    public Text title, difficulty;
    public WWW www;
    public string url;

    public void SetText(string title, int difficulty)
    {
        this.title.text = title;
        this.difficulty.text = difficulty.ToString();
    }

    public void Play()
    {
            wrapper.data = filesystem.Load_MusicSheet(path.sheetpath);
            if (wrapper.data != null)
            {
                string uri = "file:///" + path.musicpath;
                url = uri; 
                Debug.Log(uri);
                UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(uri, G.CRAF.mtype[path.musictype]);
                StartCoroutine(getaudioclip(req));
                //StartCoroutine(download());
        }
    }

    private IEnumerator download()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().clip = www.GetAudioClip();
        wrapper.musicclip = www.GetAudioClip();
        yield return www;

    }

    private IEnumerator getaudioclip(UnityWebRequest req)
    {
        yield return req.SendWebRequest();
        if (req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            wrapper.musicclip= DownloadHandlerAudioClip.GetContent(req);
            Debug.Log(true);
            SceneManager.LoadScene("game");
        }
    }
}
