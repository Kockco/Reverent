using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    new AudioSource audio;
    AudioClip audioClip;
    VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audioClip = GetComponent<AudioSource>().clip;

    }

    // Update is called once per frame
    void Update()
    {
        if (!video.isPlaying)
            SceneManager.LoadScene(1);
    }
}
