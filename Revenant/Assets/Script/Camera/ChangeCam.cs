using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public int situation;
    public Camera[] cam;
    public CameraPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = player.GetComponent<CameraPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.otherCamera = true;
            cam[0].enabled = false;
            if (situation == 0)
            {
                player.transform.GetChild(0).gameObject.SetActive(false);
                cam[1].enabled = true;
            }
            if (situation == 1)
                cam[2].enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.otherCamera = false;
            
            cam[0].enabled = true;
            if (situation == 0)
            {
                player.transform.GetChild(0).gameObject.SetActive(true);
                cam[1].enabled = false;
            }
            if (situation == 1)
                cam[2].enabled = false;
        }
    }
}
