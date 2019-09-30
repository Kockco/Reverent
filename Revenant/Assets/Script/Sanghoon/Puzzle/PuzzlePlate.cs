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
    //근처 지점
    public bool isRock;
    public float nearAngle = 360;
    public float myRot;
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
        isRock = true;
    }
    private void Update()
    {
        if(transform.eulerAngles.y < 0)
        {
            myRot = 180 + transform.eulerAngles.y;
        }
        else
        {
            myRot = transform.eulerAngles.y;
        }
        // 핸들과 판이 같이 움직이도록
        if(link_handle.isCatch)
            transform.rotation = link_handle.gameObject.transform.rotation;

        else if(!link_handle.isCatch && !isRock)
        {
            //정해진 위치면?
            foreach(float rot in stopAngle)
            {
                if (transform.eulerAngles.y < rot + 1 && transform.eulerAngles.y > rot - 1)
                {
                    isRock = true;
                    //포테이토 부모님 바꾸기
                    foreach (GameObject pot in link_handle.potato)
                    {
                        pot.transform.parent = link_handle.potatoParent.transform;
                    }
                }

                else
                {
                    switch (potatoCount)
                    {
                        case 2:
                            if (myRot < stopAngle[1] + 90 && myRot > stopAngle[1] - 90)
                                nearAngle = stopAngle[1];
                            else
                                nearAngle = stopAngle[0];
                            break;
                        case 3:
                            if (myRot <= stopAngle[2] + 60 && myRot >= stopAngle[2] - 60)
                                nearAngle = stopAngle[2];
                            else if (myRot <= stopAngle[1] + 60 && myRot >= stopAngle[1] - 60)
                                nearAngle = stopAngle[1];
                            else
                                nearAngle = stopAngle[0];
                            break;
                        case 4:
                            if (myRot <= stopAngle[3] + 45 && myRot >= stopAngle[3] - 45)
                                nearAngle = stopAngle[3];
                            else if (myRot <= stopAngle[2] + 45 && myRot >= stopAngle[2] - 45)
                                nearAngle = stopAngle[2];
                            else if (myRot <= stopAngle[1] + 45 && myRot >= stopAngle[1] - 45)
                                nearAngle = stopAngle[1];
                            else
                                nearAngle = stopAngle[0];
                            break;
                    }
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, nearAngle, transform.rotation.z), 3 *Time.deltaTime);
            }
            
        }
    }
}
