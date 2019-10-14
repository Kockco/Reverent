using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
    //현재 위치값
    public int myNum;
    public int resultNum;
    public GameObject[] potatoPos;


    void Start()
    {
        potatoPos = GameObject.FindGameObjectsWithTag("Potato_Collider");
        transform.position = potatoPos[myNum].transform.position;
        transform.rotation = potatoPos[myNum].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PositionReset()
    {
        transform.position = potatoPos[myNum].transform.position;
    }
}
