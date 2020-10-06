using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSprite : MonoBehaviour
{
    public GameObject[] checkPic = new GameObject[4];
    public int type;
    private void Start()
    {
        type = -1;
    }
    // Update is called once per frame
    void Update()
    {
        if (type!=-1)
        {
            checkPic[type].SetActive(true);
            StartCoroutine(showpicture());
        }
    }

    IEnumerator showpicture()
    {
        yield return new WaitForSeconds(0.1f);
        checkPic[type].SetActive(false);
        type = -1;
    }
}
