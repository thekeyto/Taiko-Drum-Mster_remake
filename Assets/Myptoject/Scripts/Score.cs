using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    public scoreSystem grade;
    public Text[] text = new Text[5];
    void Start()
    {
        grade = GameObject.Find("score").GetComponent<scoreSystem>();
        text[0].text = grade.perfect.ToString();
        text[1].text = grade.great.ToString();
        text[2].text = grade.good.ToString();
        text[3].text = "最大combo："+grade.maxcombo.ToString();
        text[4].text = "最终成绩："+grade.grade.ToString();
    }

    public void backToMenu()
    {
        Destroy(GameObject.Find("score"));
        SceneManager.LoadScene("initial");
    }
}
