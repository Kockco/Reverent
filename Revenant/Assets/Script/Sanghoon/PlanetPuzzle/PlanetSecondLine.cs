using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSecondLine : MonoBehaviour
{
    PlanetCollision planetCol;
    //자식으로 있냐없냐 판단
    public bool isChild;

    public GameObject planet;

    //핸들과 연결
    public PlantPuzzleHandle handle;
    public float rotateSpeed;

    [SerializeField]
    public float[] stopAngle;
    [SerializeField]
    public float[] centerAngle;
    public int cutAngle;
    public int myPoint;
    
    //현재 멈춰있는지 알기위함
    public bool isLock;

    public int firstChangePoint;
    public int secondChangePoint;

    void Awake()
    {
        planetCol = planet.GetComponent<PlanetCollision>();

        isChild = true;
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
        transform.rotation = startingY;
    }

    void Update()
    {
        Debug.Log(name + transform.eulerAngles.y);
        if (isChild)
            AutioRotation();
    }

    public void PlanetRotate(float direction)
    {
        transform.Rotate(0, rotateSpeed * direction * Time.deltaTime, 0);
        ChildRotation(direction);
    }

    //왼쪽으로 맟출건지 오른쪽으로 맟출건지 결정
    public void AngleCheck()
    {
        for (int i = 0; i < cutAngle; i++)
        {
            if (i == cutAngle - 1)
            {
                if (transform.eulerAngles.y > centerAngle[i] || transform.eulerAngles.y < centerAngle[0])
                {
                    myPoint = 0;
                }
            }
            else
            {
                if (transform.eulerAngles.y > centerAngle[i] && transform.eulerAngles.y < centerAngle[i + 1])
                {
                    myPoint = i + 1;
                }
            }
        }
    }

    //자전
    void ChildRotation(float direction)
    {
        planet.transform.Rotate(0, rotateSpeed * direction * Time.deltaTime, 0);
    }

    //자동으로 움직이기
    void AutioRotation()
    {
        if (!handle.isCatch)
        {
            if (!isLock)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, stopAngle[myPoint], transform.rotation.z), rotateSpeed * 0.5f * Time.deltaTime);
                ChildRotation(rotateSpeed * Time.deltaTime);
                if ((int)transform.eulerAngles.y == stopAngle[myPoint])
                {
                    isLock = true;
                }
            }
        }
    }

    public void LineCheck()
    {
        if(planetCol.onChange )
        {
            if(planetCol.number ==1)
            {
                myPoint = firstChangePoint;
            }
            else if (planetCol.number == 2)
            {
                myPoint = secondChangePoint;
            }

            planet.transform.SetParent(transform);
        }
    }
}
