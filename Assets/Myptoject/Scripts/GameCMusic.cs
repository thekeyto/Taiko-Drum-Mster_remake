using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCMusic : MonoBehaviour
{
    public GameObject tempitem;
    public GameObject itemgroup;
    public SheetWrapper data;


    public NowLoading loading;
    public MessageBox messagebox;

    private Vector3 itemgroup_pos;
    private readonly float itemmargin = 10f;

    private void Awake()
    {

        int i = 0;

        foreach(var item in filesystem.musicsheet_lib)
        {
            var new_item = Instantiate(tempitem);
            new_item.transform.SetParent(itemgroup.transform);
            i++;
        }
        Destroy(tempitem);
    }

    private void Start()
    {
        messagebox.Show(G.lang.message_all_musicsheet_loaded[G.CURLANG] + filesystem.musicsheet_lib.Count.ToString());
    }

    public void BackMenu()
    {
        Destroy(data.gameObject);
        SceneManager.LoadScene("Menu");
    }
}
