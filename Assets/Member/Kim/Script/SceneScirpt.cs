using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScirpt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainScene()
    {
        SceneManager.LoadScene("Main"); 
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
