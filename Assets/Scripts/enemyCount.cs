using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemyCount : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] Text txtscore;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
       score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
        Debug.Log(main.name + " has " + main.transform.childCount + " children");
        if(main.transform.childCount == 4)
        {
            score = 100;
        }
        if(main.transform.childCount == 3)
        {
            score = 200;
        }
        if(main.transform.childCount == 2)
        {
            score = 300;
        }
        if(main.transform.childCount == 1)
        {
            score = 400;
        }
        if(main.transform.childCount == 0)
        {
            score = 500;
        }
        if(main.transform.childCount == 0)
        {
            SceneManager.LoadSceneAsync(2);
        }
        txtscore.text = "Score:  " + score.ToString();
    }
}
