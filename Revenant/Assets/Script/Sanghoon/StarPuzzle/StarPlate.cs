using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlate : MonoBehaviour
{
    //핸들과 연결
    public StarHandle handle;
    public float rotateSpeed;
    
    [SerializeField]
    public float[] stopAngle;
    [SerializeField]
    public float[] centerAngle;
    public int cutAngle;
    public int myPoint;

    //현재 멈춰있는지 알기위함
    public bool isLock;

    // Start is called before the first frame update
    void Start()
    {
        //멈추는 지점
        stopAngle = new float[cutAngle];
        //중앙지점(왼쪽으로갈건지 오른쪽으로 갈건지 결정)
        centerAngle = new float[cutAngle];

        //360 / 잘린갯수만큼 계산
        float startAngle = 360 / cutAngle;
        for (int i = 0; i < cutAngle; i++)
        {
            stopAngle[i] = i * startAngle;
            centerAngle[i] = (startAngle / 2) + startAngle * i;
        }
        //시작할때 지점
        Quaternion startingY = Quaternion.Euler(0, stopAngle[myPoint], 0);
        transform.localRotation = startingY;

    }

    // Update is called once per frame
    void Update()
    {
        AutoRotation();
    }

    public void StarRotate(float direction)
    {
        transform.Rotate(0, rotateSpeed * direction * Time.deltaTime, 0);
    }

    //왼쪽으로 맟출건지 오른쪽으로 맟출건지 결정
    public void AngleCheck()
    {
        for (int i = 0; i < cutAngle; i++)
        {
            if (i == cutAngle - 1)
            {
                if (transform.localRotation.eulerAngles.y > centerAngle[i] || transform.localRotation.eulerAngles.y < centerAngle[0])
                {
                    myPoint = 0;
                }
            }
            else
            {
                if (transform.localRotation.eulerAngles.y > centerAngle[i] && transform.localRotation.eulerAngles.y < centerAngle[i + 1])
                {
                    myPoint = i + 1;
                }
            }
        }
    }

    //자동으로 움직이기
    void AutoRotation()
    {
        if (!handle.isCatch)
        {
            if (!isLock)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.x, stopAngle[myPoint], transform.localRotation.z), rotateSpeed * 0.5f * Time.deltaTime);
                if ((int)transform.localRotation.eulerAngles.y == stopAngle[myPoint])
                {
                    isLock = true;
                }
            }
        }
    }

}
