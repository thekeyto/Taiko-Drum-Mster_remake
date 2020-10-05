using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public GameObject[] image = new GameObject[4];
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            image[0].SetActive(true);
            StartCoroutine(waitfortime(0));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            image[1].SetActive(true);
            StartCoroutine(waitfortime(1));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            image[2].SetActive(true);
            StartCoroutine(waitfortime(2));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            image[3].SetActive(true);
            StartCoroutine(waitfortime(3));
        }
    }

    IEnumerator waitfortime(int id)
    {
        yield return new WaitForSeconds(0.1f);
        image[id].SetActive(false);
    }
}
