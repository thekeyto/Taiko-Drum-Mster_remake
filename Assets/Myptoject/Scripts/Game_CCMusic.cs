using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_CCMusic : MonoBehaviour
{
    public GameObject musicitem;
    public GameObject itemgroup;

    public MessageBox msgbox;
    public AudioSource music; 

    private void Awake()
    {
        var musiclist = filesystem.LoadCraftTempMusic();//读取音乐
        if (musiclist != null)
        {
            foreach (var (musicpath, musicname, extension) in musiclist)
            {
                var newitem = Instantiate(musicitem);
                newitem.transform.SetParent(itemgroup.transform);
                newitem.GetComponent<CCMusicItem>().Set(musicname, musicpath, extension);

            }
        }
        Destroy(musicitem);
        music.volume = G.setting.musicvolume;
    }
    public void BackToMenu()
    {
        Destroy(music.gameObject);
        SceneManager.LoadScene("Menu");
    }
}
