using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRestartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void startButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void restartButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
}