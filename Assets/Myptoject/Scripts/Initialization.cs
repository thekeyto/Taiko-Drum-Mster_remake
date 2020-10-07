using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    void Start()
    {
        try { 
        filesystem.LoadSetting();
        G.DATA_PATH = Application.dataPath;
        G.VERSION = Application.version;

        Debug.Log("Application.dataPath" + Application.dataPath);
        filesystem.Load_AllSheets();
        SceneManager.LoadScene("Menu");
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            SceneManager.LoadScene("Menu");
        }
    }
}