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
    public GameObject desFat;
    void Start()
    {
        desFat = GameObject.Find("desFat");
        isactive = false;
        render = this.GetComponent<SpriteRenderer>();
        render.sprite = notactive;
    }

    // Update is called once per frame
    void Update()
    {
        if (isactive==true)
        {
            this.transform.SetParent(desFat.transform);
            Destroy(this.gameObject, 0.2f);
            render.sprite = active;
        }
    }
}
