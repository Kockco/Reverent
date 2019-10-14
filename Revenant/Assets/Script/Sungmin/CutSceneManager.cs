using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public float videoTime;
    VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();

        Invoke("OpeningEnd", 40);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    videoTime += Time.deltaTime;

    //    if (!video.isPlaying && videoTime >= 5)
    //        OpeningEnd();
    //}

    void OpeningEnd()
    {
        SceneManager.LoadScene(1);
    }
}
