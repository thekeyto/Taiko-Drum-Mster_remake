using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Group_Editing : MonoBehaviour
{
    public Game_Adjust game;
    public void ShiftUp()
    {
        foreach (var n in game.selected)
        {
            n.atime += 0.02f;
        }
        Reposition();
    }

    public void ShiftDown()
    {
        foreach (var n in game.selected)
        {
            n.atime -= 0.02f;
        }
        Reposition();
    }

    public void ShiftLeft()
    {
        Reposition();
    }
    private void Reposition()
    {
        foreach (var n in game.selected)
        {
            if (game.taplist.Contains(n)) { game.ResetNote(n); }
        }
    }
}
