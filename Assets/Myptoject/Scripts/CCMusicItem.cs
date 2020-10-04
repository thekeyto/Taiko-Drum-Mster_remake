using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CCMusicItem : MonoBehaviour
{
    public Text title;
    public string musicpath;
    public string musictype;
    public AudioSource music;

    public void Set(string filename, string musicpath, string extension)
    {
        this.title.text = filename;
        this.musicpath = musicpath;
        this.musictype = extension;
    }

    public void Click()
    {
        if (File.Exists(musicpath))
        {
            string uri = "file:///" + musicpath;
            Debug.Log(uri);
            UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(uri, G.CRAF.mtype[musictype]);
            StartCoroutine(getaudioclip(req));
        }
    }

    private IEnumerator getaudioclip(UnityWebRequest req)
    {
        yield return  req.SendWebRequest();
        if (req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            G.CRAF.currentmusic_path = musicpath;
            music.clip = DownloadHandlerAudioClip.GetContent(req);
            SceneManager.LoadScene("craftMusic");
        }
    }
}
