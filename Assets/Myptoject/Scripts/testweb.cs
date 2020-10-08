using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class testweb: MonoBehaviour
{
    public AudioSource music;
    private void Start()
    {
        if(File.Exists("D:\\myproject\\TDM_remake\\Assets\\craft\\Aiobahn_Yunomi_ボンジュール鈴木-マジカルスイートケーキ-_Stripe.P-Remix__1.wav"))
        {
        string uri = "file:///D:\\myproject\\TDM_remake\\Assets\\craft\\Aiobahn_Yunomi_ボンジュール鈴木-マジカルスイートケーキ-_Stripe.P-Remix__1.wav";
        Debug.Log(uri);
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.WAV);
        StartCoroutine(getaudioclip(req));
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
            music.clip = DownloadHandlerAudioClip.GetContent(req);
            Debug.Log(true);
        }
    }
}
