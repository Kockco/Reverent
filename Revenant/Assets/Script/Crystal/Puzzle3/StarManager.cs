﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    //단계별로 구성 1,2,3단계
    public int page = 1;

    public GameObject[] cc;
    public GameObject[] ec;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 2; i < 9; i++)
        {
            cc[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (page == 1 && ec[6].GetComponent<CrystalStarPuzzle>().GetState(1) && ec[0].GetComponent<CrystalStarPuzzle>().GetState(2))
        {
            page = 2;
            NextPage();
            ec[6].GetComponent<CrystalStarPuzzle>().SetInit();
            ec[0].GetComponent<CrystalStarPuzzle>().SetInit();
        }
    }
    void NextPage()
    {
        switch (page)
        {
            case 1:
                break;
            case 2:
                cc[0].SetActive(false);
                cc[1].SetActive(false);
                cc[2].SetActive(true);
                cc[3].SetActive(true);
                cc[4].SetActive(true);
                break;
            case 3:
                cc[2].SetActive(false);
                cc[3].SetActive(false);
                cc[4].SetActive(false);
                cc[5].SetActive(true);
                cc[6].SetActive(true);
                cc[7].SetActive(true);
                cc[8].SetActive(true);
                break;
            case 4:
                cc[5].SetActive(false);
                cc[6].SetActive(false);
                cc[7].SetActive(false);
                cc[8].SetActive(false);
                break;
        }

    }
}
