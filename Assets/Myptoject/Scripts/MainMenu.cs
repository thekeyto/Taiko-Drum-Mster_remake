using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadScene("ChooseMusic");
    }
    public void makemusic()
    {
        SceneManager.LoadScene("chooseCraftMusic");
    }
    public void exitgame()
    {
        Application.Quit();
    }
}
