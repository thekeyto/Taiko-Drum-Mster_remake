using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCMusic : MonoBehaviour
{
    public GameObject tempitem;
    public GameObject itemgroup;
    public SheetWrapper data;

    private Vector3 itemgroup_pos;
    private readonly float itemmargin = 10f;

    private void Awake()
    {

        foreach(var item in filesystem.musicsheet_lib)
        {
            var new_item = Instantiate(tempitem);
            new_item.transform.SetParent(itemgroup.transform);

            Debug.Log(item.Key.ToString());
            var summary = item.Value;
            new_item.GetComponent<MusicItem>().path = item.Key;
            new_item.GetComponent<MusicItem>().SetText(summary.title, summary.difficulty);
        }
        Destroy(tempitem);
    }
    public void BackMenu()
    {
        Destroy(data.gameObject);
        SceneManager.LoadScene("Menu");
    }
}
