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

    public void SetText(string title, int difficulty)
    {
        this.title.text = title;
        this.difficulty.text = difficulty.ToString();
    }

    public void Play()
    {
        // Load the sheet data
        try
        {
            sheet sheet = filesystem.Load_MusicSheet(path.sheetpath);
            wrapper.data = sheet;
            if (sheet != null)
            {
                // Load music using web request
                string uri = "file:///" + path.musicpath;
                Debug.Log(uri);
                UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(uri, G.CRAF.mtype[path.musictype]);
                StartCoroutine(getaudioclip(req));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
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
            wrapper.musicclip = DownloadHandlerAudioClip.GetContent(req);
            SceneManager.LoadScene("game");
        }
    }
}
