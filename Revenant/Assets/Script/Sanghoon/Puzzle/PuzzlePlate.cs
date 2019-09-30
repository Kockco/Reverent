using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlate : MonoBehaviour
{
    [Header("퍼즐 링크")]
    public PuzzleHandle link_handle;
    [Header("붙어있는 감자 갯수")]
    public int potatoCount;
    //멈추는 지점
    public float[] stopAngle;
    
    // Start is called before the first frame update
    void Awake()
    {
        //멈추는 지점
        stopAngle = new float[potatoCount];

        //360 / ? 로 각도 정하기
        float angle = 360;
        if (potatoCount != 0)
            angle /= potatoCount;

        //현재 y로테이션 값에서  360 나눠 구하기
        float startAngle = transform.eulerAngles.y;
        for(int i = 0; i < potatoCount; i++)
        {
            if((angle * i) + startAngle < 360)
            stopAngle[i] = (angle * i)+ startAngle;
            else
            {
                stopAngle[i] = (angle * i) + startAngle - 360;
            }
        }

        if (link_handle == null)
        {
            Debug.Log("not link handle");
        }
        else
        {
            link_handle.link_plate = this;
        }
    }
}
