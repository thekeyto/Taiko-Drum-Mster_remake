using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        filesystem.LoadSetting();
        G.DATA_PATH = Application.dataPath;
        G.VERSION = Application.version;

        Debug.Log("Application.dataPath" + Application.dataPath);
    }
    void Start()
    {
        filesystem.Load_AllSheets();
    }
}
