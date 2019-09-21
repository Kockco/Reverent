using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    enum InOut
    {
        COL_IN,
        COL_OUT
    }
    public float FadeTime = 0.2f; // Fade효과 재생시간

    public Image fadeImg;
    float start;
    float time = 0f;
    bool isPlaying = false;
    bool exit = false;
    InOut col;
    private void Start()
    {
        exit = false;
    }

    private void Update()
    {
        if(isPlaying == true)
        {
            OutStartFadeAnim();
        }
    }

    public void OutStartFadeAnim()
    {
        if (time < FadeTime)
        {
            fadeImg.GetComponent<Animator>().SetBool("In", true);
            time += Time.deltaTime;
        }
        if(time > FadeTime)
        {
            fadeImg.GetComponent<Animator>().SetBool("In", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isPlaying = true;
        col = InOut.COL_IN;
    }
    private void OnTriggerExit(Collider other)
    {
        isPlaying = true;
        col = InOut.COL_OUT;
    }
}
