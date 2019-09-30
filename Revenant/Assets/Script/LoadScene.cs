using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
            SceneManager.LoadScene("GrayBoxingScene");
        if (Input.GetKeyDown(KeyCode.F11))
            SceneManager.LoadScene("Level01_Tutorial");
    }
    public void ChangeGameScene()
    {
    }
}
