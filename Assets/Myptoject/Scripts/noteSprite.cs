using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class noteSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite notactive;
    public Sprite active;
    public bool isactive;
    public SpriteRenderer render;
    void Start()
    {
        isactive = false;
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = notactive;
    }

    // Update is called once per frame
    void Update()
    {
        if (isactive==true)
        {
            render.sprite = active;
        }
    }
}
