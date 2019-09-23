using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamera : MonoBehaviour
{
    CameraPlayer playerCam;
    float time;
    bool oneIn;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.Find("PC").GetComponent<CameraPlayer>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(oneIn)
        {
            time += Time.deltaTime;
            if(time >0.3f)
            {
                    playerCam.topView = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        oneIn = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            time = 0;
            playerCam.topView = false;
            oneIn = false;
        }
    }
}
