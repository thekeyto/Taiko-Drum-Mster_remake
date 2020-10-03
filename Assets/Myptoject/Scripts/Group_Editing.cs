using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Group_Editing : MonoBehaviour
{
    public Game_Adjust game;
    public SelectionControl sc;

    public Button p1btn, p2btn, p3btn, p4btn, mhbtn, mvbtn, esbtn;
    public void On()
    {
        p1btn.interactable = true; p2btn.interactable = true; p3btn.interactable = true; p4btn.interactable = true;
        mhbtn.interactable = true; mvbtn.interactable = true; esbtn.interactable = true;
    }
    public void Off()
    {
        p1btn.interactable = false; p2btn.interactable = false; p3btn.interactable = false; p4btn.interactable = false;
        mhbtn.interactable = false; mvbtn.interactable = false; esbtn.interactable = false;
    }
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
        foreach (var n in game.selected)
        {
            n.lane = (n.lane + G.LANES - 1) % G.LANES;
        }
        Reposition();
        sc.DeSelect();
    }

    public void ShiftRight()
    {
        foreach (var n in game.selected)
        {
            n.lane = (n.lane + 1) % G.LANES;
        }
        Reposition();
        sc.DeSelect();
    }
    private void Reposition()
    {
        foreach (var n in game.selected)
        {
            if (game.taplist.Contains(n)) { game.ResetNote(n); }
        }
    }
}
