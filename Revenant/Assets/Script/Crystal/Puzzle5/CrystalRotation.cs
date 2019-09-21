using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotation : CrystalPuzzle
{
    public enum STATE
    {
        NORMAL,
        RIGHT,
        LEFT,
        END,
        PAUSE,
        RESET,
        CLEAR
    }
    public Transform parent;

    public float RotY;
    [Range(0, -179)]
    public int minLimit;
    [Range(0, 179)]
    public int maxLimit;
    public int stopPointDistance = 30;

    public int myPoint;
    int zeroPoint;
    int stopPointCount = 0;
    public int[] stopPoint;
    public float movingTime;
    float myTime;
    public STATE state;
    public bool isLeft = false; //왼쪽오른쪽 맡겨놓기
    private void Start()
    {
        isLeft = false;
        RotY = 0;
        c_state = GetComponent<EmptyCrystal>();
        c_state.state = C_STATE.EMPTY;

        parent = transform.parent;
        movingTime = 1;
        myTime = 0;
        state = STATE.NORMAL;
        //왼쪽 오른쪽 최대 지점을 정하고 구간계산
        if ((maxLimit + (-minLimit)) % stopPointDistance == 0)
        {
            stopPointCount = ((maxLimit + (-minLimit)) / stopPointDistance) + 1; //0때문에 1더함
            stopPoint = new int[stopPointCount];
            int section = (maxLimit + (-minLimit)) / (stopPointCount - 1);
            for (int i = 0; i < stopPointCount; i++)
            {
                stopPoint[i] = (section * i) + minLimit;
                if (parent.rotation.eulerAngles.y > 180)
                {
                    if (stopPoint[i] == (int)transform.parent.rotation.eulerAngles.y -360)
                    {
                        myPoint = i;
                        zeroPoint = i;
                        Debug.Log(gameObject.name + (int)transform.parent.rotation.eulerAngles.y);
                    }
                }
                else
                {
                    if (stopPoint[i] == (int)transform.parent.rotation.eulerAngles.y)
                    {
                        myPoint = i;
                        zeroPoint = i;
                        Debug.Log(gameObject.name + (int)transform.parent.rotation.eulerAngles.y);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Stop Point Error");
            stopPoint = null;
        }

        if (transform.parent == null)
            Debug.Log("parent not find");

        if (minLimit >= maxLimit)
            Debug.Log("Limit Error");
    }
    private void Update()
    {
        StairChange();
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
        RotationArrive();
        switch (state)
        {
            case STATE.RESET:
                if (RotY < stopPoint[zeroPoint] - 1)
                {
                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 20);
                }
                else if (RotY > stopPoint[zeroPoint] + 1)
                {
                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 20);
                }
                else
                {
                    myPoint = zeroPoint;
                    parent.transform.rotation = Quaternion.Euler(new Vector3(0,stopPoint[zeroPoint],0));
                    state = STATE.NORMAL;
                }
                break;
            case STATE.RIGHT:
                if (stopPoint[myPoint + 1] >= RotY) //자기보다 한칸 위쪽포지션까지이동
                {
                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 20);
                }
                else
                {
                    myPoint += 1;
                    state = STATE.PAUSE;
                    isLeft = false;
                }
                break;
            case STATE.LEFT:
                if (stopPoint[myPoint - 1] <= RotY)
                {
                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 20);
                }
                else
                {
                    myPoint -= 1;
                    state = STATE.PAUSE;
                    isLeft = true;
                }
                break;
            case STATE.PAUSE:
                myTime += Time.deltaTime;
                if (myTime > movingTime)
                {
                    if (isLeft)
                        state = STATE.LEFT;
                    else
                        state = STATE.RIGHT;

                    myTime = 0;
                }
                break;
            case STATE.END:
                break;
        }
    }

    void RotationArrive()
    {
        if ((state == STATE.RIGHT && myPoint == stopPointCount - 1) ||
            (state == STATE.LEFT && myPoint == 0)) // 일단 끝이면 아무것도안함
        {
            state = STATE.END;
        }
    }
}
