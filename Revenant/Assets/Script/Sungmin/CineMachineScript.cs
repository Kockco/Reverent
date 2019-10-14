using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

public class CineMachineScript : MonoBehaviour
{
    public GameObject[] starPuzzles;
    public PlayableDirector play;
    public TimelineAsset[] timeline;

    bool switchingCam;

    // Start is called before the first frame update
    void Start()
    {
        InitStarPuzzle();
    }

    public void InitStarPuzzle()
    {
        for (int i = 0; i < starPuzzles.Length; i++)
            starPuzzles[i].SetActive(false);
    }

    public void CineCameraSwitching()
    {
        if (!switchingCam)
        {
            GetComponent<GameObject>().SetActive(true);
            Camera.main.transform.gameObject.SetActive(false);
        }
        else
        {
            Camera.main.transform.gameObject.SetActive(true);
            GetComponent<GameObject>().SetActive(false);
        }
    }

    public void PlayPuzzleCine()
    {
        for (int i = 0; i < starPuzzles.Length; i++)
            starPuzzles[i].SetActive(true);


        Invoke("InitStarPuzzle", 20);
        // play.Play();
    }

    public void ClearPuzzleCine(int puzzleNum)
    {
        play.playableAsset = timeline[puzzleNum - 1];
        play.Play();
    }

    public void EndTimeLine()
    {
        Camera.main.transform.gameObject.SetActive(true);
        GetComponent<GameObject>().SetActive(false);
    }


}
