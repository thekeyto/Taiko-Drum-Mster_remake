using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWrapper : MonoBehaviour
{
    public List<(int ,double)> taplist;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
