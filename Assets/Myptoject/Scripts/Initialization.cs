using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initialization : MonoBehaviour
{
    public Scrollbar progress;
    public Language language_file;
    public NowLoading loading;
    // Start is called before the first frame update
    private void Awake()
    {
        filesystem.LoadSetting();

        G.lang = language_file;
        G.DATA_PATH = Application.dataPath;
        G.VERSION = Application.version;

        Debug.Log("Application.dataPath" + Application.dataPath);
    }
    void Start()
    {
        filesystem.Load_AllSheets();
        loading.LoadNextScene("Menu");
    }
}
