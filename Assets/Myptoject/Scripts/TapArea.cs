using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapArea : MonoBehaviour
{
    public int index;
    public Game game;
    public KeyCode keycode;
    
    private void OnKeyDown()
    {
        PointerDown();
    }
    private void OnKeyUp()
    {
        PointerUp();
    }

    public void PointerDown()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        game.hold[index] = true;

        double ct = game.timer;

        int i = 0;

        if (game.active[index].Count!=0)
        {
            Note target_active_note = game.active[index].Peek();

            if (target_active_note.interval.WhthinBound(ct))
            {
                var (points, text) = GetAccuracy(ct, target_active_note.atime); ;
                game.active[index].Dequeue();
                game.points += points * (game.combo / 10 + 1);
                game.t_points.text = game.points.ToString();

                target_active_note.noteobj.transform.localScale = new Vector3(0f, 0f, 0f);
                target_active_note.noteobj.GetComponent<ParticleSystem>().Stop();
                game.se[index].Play();
            }
        }
    }

    public void PointerUp()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        game.hold[index] = false;
    }

    private (int points,string text) GetAccuracy(double time,double atime)
    {
        double sub = Math.Abs(time - atime);
        if (sub<=G.PERFECT_MARGIN)
        {
            game.combo += 1;
            return ((int)(G.POINT_BASE / (sub + 1f)), "Perfect");
        }
        else if (sub<=G.GOOD_MARGIN)
        {
            game.combo += 1;
            return ((int)(G.POINT_BASE / (sub + 1f)), "Good");
        }
        else
        {
            game.combo = 0;
            return (0, "Miss");
        }
    }
}
