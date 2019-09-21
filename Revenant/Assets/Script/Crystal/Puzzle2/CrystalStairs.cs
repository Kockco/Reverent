using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalStairs : CrystalPuzzle
{
    public GameObject[] stairs;
    public float[] height;

    private void Start()
    {
        c_state = GetComponent<EmptyCrystal>();
        c_state.state = C_STATE.EMPTY;
        stairs = new GameObject[4];
        height = new float[2];
        height[0] = transform.position.y - 2.3f;
        height[1] = height[0] + 2.45f;
        for (int i = 1; i < 5 /*stairs.length*/; i++)
            stairs[i-1] = transform.GetChild(i).gameObject;
    }
    private void Update()
    {
        //if(c_state.isActive == true && transform.GetComponent<EmptyCrystal>().delay >0.5f)
            StairChange();
    }

    void StairChange()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i != (int)c_state.state)
                stairs[i].transform.position = new Vector3(stairs[i].transform.position.x, height[0], stairs[i].transform.position.z);
        }
        switch (c_state.state)
        {
            case C_STATE.EMPTY:
                c_state.isActive = false;   
                break;
            case C_STATE.BLUE:
                StairsArrive(stairs[0]);
                stairs[0].SetActive(true);
                if (stairs[0].transform.position.y <= height[1]) // (이부분 수정함 땃쥐야)
                    stairs[0].transform.Translate(0, 10 * Time.deltaTime, 0);
                break;
            case C_STATE.WHITE:
                StairsArrive(stairs[1]);
                stairs[1].SetActive(true);
                if (stairs[1].transform.position.y <= height[1])// (이부분 수정함 땃쥐야)
                    stairs[1].transform.Translate(0, 2 * Time.deltaTime, 0);
                break;
            case C_STATE.RED:
                StairsArrive(stairs[2]);
                stairs[2].SetActive(true);
                if (stairs[2].transform.position.y <= height[1])// (이부분 수정함 땃쥐야)
                    stairs[2].transform.Translate(0, 2 * Time.deltaTime, 0);
                break;
            case C_STATE.BLACK:
                StairsArrive(stairs[3]);
                stairs[3].SetActive(true);
                if (stairs[3].transform.position.y <= height[1])// (이부분 수정함 땃쥐야)
                    stairs[3].transform.Translate(0, 2 * Time.deltaTime, 0);
                break;
        }
    }

    void StairsArrive(GameObject stair)
    {
        if(stair.transform.position.y >= height[1])
        {
            c_state.isActive = false;
        }
    }
}
