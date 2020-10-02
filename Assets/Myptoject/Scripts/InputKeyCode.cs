using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyCode : MonoBehaviour
{
    public bool[] keycodes = new bool[5]; 
    private void Start()
    {
        for (int i = 0; i < 4; i++) keycodes[i] = false;
    }

    private void Update()
    {
        
    }
}
