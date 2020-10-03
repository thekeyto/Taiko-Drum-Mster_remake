using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_CCMusic : MonoBehaviour
{
    public GameObject musicitem, esheetitem;
    public GameObject itemgroup, eitemgruop;

    public MessageBox msgbox;
    public NowLoading loading;
    public AudioSource music;

    private Vector3 anchor;
    private readonly int columns = 4;
    private readonly float width = 340f, height = 110f;

    private void Awake()
    {
        anchor = musicitem.transform.localPosition;
        var musiclist = filesystem.LoadCraftTempMusic();//读取音乐
        if (musiclist != null)
        {
            int r = 0, c = 0;
            foreach(var (musicpath, musicname, extension) in musiclist)
            {
                var newitem = Instantiate(musicitem);
                newitem.transform.SetParent(itemgroup.transform);
                newitem.GetComponent<CCMusicItem>().Set(musicname, musicpath, extension);
				newitem.transform.localPosition = new Vector3(anchor.x + c * width, anchor.y - r * height, anchor.z);

            }
        }
    }

    public void BackToMenu()
    {
        Destroy(music.gameObject);
        loading.LoadNextScene("Menu");
    }
}
