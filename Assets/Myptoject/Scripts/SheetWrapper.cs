using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetWrapper : MonoBehaviour
{
    public sheet data;
    public AudioClip musicclip;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
