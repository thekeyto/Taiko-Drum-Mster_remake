using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreSystem : MonoBehaviour
{
    public int maxcombo, perfect, great, good,grade;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
