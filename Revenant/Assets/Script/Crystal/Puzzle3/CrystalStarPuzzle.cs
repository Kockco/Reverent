﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CrystalStarPuzzle : MonoBehaviour
//{
//    [Header("센터 좌표")]
//    public Transform centerObject; //바닥 중앙 좌표

//    Vector3 myPos;
//    Vector3 linePos;

//    enum STATE
//    {
//        stop,
//        move,
//        right,
//        left
//    }
//    STATE state;
//    public Transform parent;

//    public float startRot;
//    public float RotY;
//    [Header("최저값 최대값")]
//    [Range(0, -179)]
//    public int minLimit;
//    [Range(0, 179)]
//    public int maxLimit;

//    LineRenderer Line;
//    [Header("최저값 최대값")]
//    public GameObject[] LinkPos;
//    [Header("최저값 최대값")]
//    public GameObject[] nextCrystal;
    
//    int posLenght;
//    private void Start()
//    {
//        //라인 위치가 좀 위로 올라가야함(피봇이 바닥)
//        linePos = transform.position;
//        linePos.y = transform.position.y + 3f;

//        //센터 포지션 맟춰주기
//        myPos = transform.position;
//        transform.parent.position = centerObject.position;
//        transform.position = myPos;
        
//        RotY = 0;
//        state = STATE.stop;
//        //부모 돌려주기
//        parent = transform.parent;
//        startRot= parent.rotation.eulerAngles.y;

//        //선 초기화
//        Line = GetComponent<LineRenderer>();
//        Line.startWidth = .15f;
//        Line.endWidth = .15f;
//        posLenght = LinkPos.Length;

//        if (parent.rotation.eulerAngles.y > 180)
//        {
//            startRot = parent.rotation.eulerAngles.y - 360;
//        }
//        else
//        {
//            startRot = parent.rotation.eulerAngles.y;
//        }

//        if (transform.parent == null)
//            Debug.Log("parent not find");

//        if (minLimit >= maxLimit)
//            Debug.Log("Limit Error");
//    }

//    private void Update()
//    {
//            for (int i = 0; i < posLenght; i++)
//            {
//                //링크된애들이랑 번호가 같으면 그쪽으로 연결해준다.
//                if (transform.GetComponent<CrystalState>().myNum == LinkPos[i].GetComponent<CrystalState>().myNum)
//                {
//                    linePos = transform.position;
//                    linePos.y = transform.position.y + 2f;
//                    Vector3 linkPos = LinkPos[i].transform.position;
//                    linkPos.y = LinkPos[i].transform.position.y +1f;
//                    Line.SetPosition(1, linePos);
//                    Line.SetPosition(0, linkPos);
//                }
//            }
//        }


//        if (parent.rotation.eulerAngles.y > 180)
//        {
//            RotY = parent.rotation.eulerAngles.y - 360;
//        }
//        else
//        {
//            RotY = parent.rotation.eulerAngles.y;
//        }
//    }

//    void StairChange()
//    {
//                if (RotY < startRot - 1)
//                {
//                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 10);
//                }
//                else if (RotY > startRot +1)
//                {
//                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 10);
//                }
//                else
//                {
//                    linePos = transform.position;
//                    linePos.y = transform.position.y + 3f;

//                    state = STATE.stop;
//                    c_state.isActive = false;

//                    Line.SetPosition(0, linePos);
//                    Line.SetPosition(1, linePos);
//                    Line.SetPosition(2, linePos);

//                }
//                if (maxLimit > RotY)
//                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 40);
//                else
//                {
//                    state = STATE.right;
//                }
//                if (minLimit < RotY)
//                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 40);
//                else
//                {
//                    state = STATE.left;
//                }
//        }
//    }
//    public bool GetState(int i)
//    {
//        switch (state)
//        {
//            case STATE.left:
//                if (i == 1)
//                    return true;
//                break;
//            case STATE.right:
//                if (i == 2)
//                    return true;
//                break;
//        }
//        return false;
//    }
//}
