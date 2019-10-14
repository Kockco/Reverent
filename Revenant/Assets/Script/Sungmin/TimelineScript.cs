using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineScript : MonoBehaviour
{
    GameObject mainCamera;
    public GameObject puzzleCamera;
    public PlayableDirector playableDirector;
    public TimelineAsset[] timeLine;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector = puzzleCamera.GetComponent<PlayableDirector>();
        mainCamera = Camera.main.transform.gameObject;

        puzzleCamera.GetComponent<Camera>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
            TimelineCamera();
    }

    void TimelineCamera()
    {
        puzzleCamera.GetComponent<Camera>().enabled = true;
        Camera.main.gameObject.SetActive(false);
        playableDirector.Play();
    }

    public void ChangeTimeline()
    {
        playableDirector.playableAsset = timeLine[1];
        playableDirector.Play();
    }

    public void EndTimeline()
    {
        mainCamera.SetActive(true);
        puzzleCamera.GetComponent<Camera>().enabled = false;
    }
}
