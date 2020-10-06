using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    void Start()
    {
        filesystem.LoadSetting();
        G.DATA_PATH = Application.dataPath;
        G.VERSION = Application.version;

        Debug.Log("Application.dataPath" + Application.dataPath);
        filesystem.Load_AllSheets();
        SceneManager.LoadScene("Menu");
    }
}
