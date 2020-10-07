using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSprite : MonoBehaviour
{
    public GameObject[] checkPic = new GameObject[6];
    public GameObject[] evaPic = new GameObject[6];
    public int type;
    // Update is called once per frame
    private void Awake()
    {
        type = -1;
    }
    void Update()
    {
        if (type!=-1)
        {
            checkPic[type].SetActive(true);
            evaPic[type].SetActive(true);
            StartCoroutine(showpicture());
        }
    }

    IEnumerator showpicture()
    {
        yield return new WaitForSeconds(0.1f);
        if (type != -1) 
        {
        checkPic[type].SetActive(false);
        evaPic[type].SetActive(false);
        }
        type = -1;
    }
}
