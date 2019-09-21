using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalStarPuzzle : CrystalPuzzle
{
    enum STATE
    {
        stop,
        move,
    }
    STATE state;
    public Transform parent;
    public float startRot;

    public float RotY;
    [Range(0, -179)]
    public int minLimit;
    [Range(0, 179)]
    public int maxLimit;

    LineRenderer Line;
    public GameObject[] LinkPos;
    public GameObject[] nextCrystal;
    int posLenght;
    private void Start()
    {
        RotY = 0;
        state = STATE.stop;
        c_state = GetComponent<EmptyCrystal>();
        c_state.state = C_STATE.EMPTY;

        parent = transform.parent;
        startRot= parent.rotation.eulerAngles.y;

        Line = GetComponent<LineRenderer>();
        Line.startWidth = .05f;
        Line.endWidth = .05f;
        posLenght = LinkPos.Length;

        if (parent.rotation.eulerAngles.y > 180)
        {
            startRot = parent.rotation.eulerAngles.y - 360;
        }
        else
        {
            startRot = parent.rotation.eulerAngles.y;
        }

        if (transform.parent == null)
            Debug.Log("parent not find");

        if (minLimit >= maxLimit)
            Debug.Log("Limit Error");
    }
    private void Update()
    {
        if (c_state.isActive == true)
        {
            state = STATE.move;
            StairChange();
            for (int i = 0; i < posLenght; i++)
            {
                if (transform.GetComponent<CrystalState>().myNum == LinkPos[i].GetComponent<CrystalState>().myNum)
                {
                    Line.SetPosition(1, transform.position);
                    Line.SetPosition(0, LinkPos[i].transform.position);
                }
                switch (transform.GetComponent<CrystalState>().myNum)
                {
                    case 401:
                        Line.SetPosition(2, nextCrystal[1].transform.position);
                        break;
                    case 402:
                        Line.SetPosition(2, nextCrystal[2].transform.position);
                        break;
                    case 403:
                        Line.SetPosition(2, nextCrystal[3].transform.position);
                        break;
                    case 404:
                        Line.SetPosition(2, transform.position);
                        break;
                }
            }
        }
        if(state == STATE.move)
            Line.enabled = false;
        else
            Line.enabled = true;


        if (parent.rotation.eulerAngles.y > 180)
        {
            RotY = parent.rotation.eulerAngles.y - 360;
        }
        else
        {
            RotY = parent.rotation.eulerAngles.y;
        }
    }

    void StairChange()
    {
        switch (c_state.state)
        {
            case C_STATE.EMPTY:
                if (RotY < startRot - 1)
                {
                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 10);
                }
                else if (RotY > startRot +1)
                {
                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 10);
                }
                else
                {
                    state = STATE.stop;
                    c_state.isActive = false;
                    Line.SetPosition(0, transform.position);
                    Line.SetPosition(1, transform.position);
                    Line.SetPosition(2, transform.position);
                }
                break;
            case C_STATE.BLUE:
                if (maxLimit > RotY)
                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 10);
                else
                {
                    state = STATE.stop;
                    c_state.isActive = false;
                }
                break;
            case C_STATE.WHITE:
                if (minLimit < RotY)
                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 10);
                else
                {
                    state = STATE.stop;
                    c_state.isActive = false;
                }
                break;
            case C_STATE.RED:
                break;
            case C_STATE.BLACK:
                break;
        }
    }
}
