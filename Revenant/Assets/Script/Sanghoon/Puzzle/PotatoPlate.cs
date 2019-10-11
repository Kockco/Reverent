using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPlate : MonoBehaviour
{
    //핸들,각도,속도,감자 [SerializeField]
    #region
    [Header("연결될 핸들")]
    [SerializeField]
    PotatoHandle handle;

    [Header("각도의 갯수")]
    [SerializeField]
    int cutAngle;

    [Header("나의 지점")]
    [SerializeField]
    int myPoint;
    int MyPoint { get; }

    [Header("회전속도")]
    [SerializeField]
    float rotateSpeed;

    [Header("제자리로 오는 속도")]
    [SerializeField]
    float returnSpeed;

    [Header("돌아야하는 각(x,y,z) 한개만 1")]
    [SerializeField]
    Vector3 rotationAngleXYZ;
    
    [Header("인접한 감자의 개수와 번호")]
    [SerializeField]
    int[] potatoNumber;
    #endregion

    //멈춰야되는 각, 중간 각, 움직이는중인지, 감자, 어느각으로 도는지?
    #region
    [HideInInspector]
    public float[] stopAngle;

    [HideInInspector]
    public float[] centerAngle;

    [HideInInspector]
    public bool isLock;

    GameObject potatoBasket;
    GameObject[] potato;

    enum AngleXYZ { X,Y,Z }
    AngleXYZ angleXYZ;
    #endregion

    void Start()
    {
        potato = GameObject.FindGameObjectsWithTag("Potato");
        potatoBasket = GameObject.Find("PotatoBasket");
        stopAngle = new float[cutAngle];
        centerAngle = new float[cutAngle];
        isLock = true;

        //360 / 잘린갯수만큼 계산
        float startAngle = 360 / cutAngle;
        for (int i = 0; i < cutAngle; i++)
        {
            stopAngle[i] = i * startAngle;
            centerAngle[i] = (startAngle / 2) + startAngle * i;
        }
        
        //시작할때 지점
        Quaternion startingAngleXYZ = Quaternion.Euler(rotationAngleXYZ  * stopAngle[myPoint] + transform.eulerAngles);
        transform.rotation = startingAngleXYZ;

        if (rotationAngleXYZ.x != 0)
            angleXYZ = AngleXYZ.X;
        else if (rotationAngleXYZ.y != 0)
            angleXYZ = AngleXYZ.Y;
        else if (rotationAngleXYZ.z != 0)
            angleXYZ = AngleXYZ.Z;
    }

    void Update()
    {
        AutoRotation();
        AngleLimit();
    }

    //자동으로 움직이기
    void AutoRotation()
    {
        if (!handle.isCatch)
        {
            if (!isLock)
            {
                switch (angleXYZ)
                {
                    case AngleXYZ.X:
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(stopAngle[myPoint],
                            transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z), returnSpeed * Time.deltaTime);
                        if ((int)transform.localRotation.eulerAngles.x == stopAngle[myPoint])
                        {
                            SetPotatoParent();
                            isLock = true;
                        }
                        break;
                    case AngleXYZ.Y:
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
                            stopAngle[myPoint], transform.localRotation.eulerAngles.z), returnSpeed * Time.deltaTime);
                        if ((int)transform.localRotation.eulerAngles.y == stopAngle[myPoint])
                        {
                            SetPotatoParent();
                            isLock = true;
                        }
                        break;
                    case AngleXYZ.Z:
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
                            transform.localRotation.eulerAngles.y, stopAngle[myPoint]), returnSpeed * Time.deltaTime);
                        if ((int)transform.localRotation.eulerAngles.z == stopAngle[myPoint])
                        {
                            SetPotatoParent();
                            isLock = true;
                        }
                        break;
                }
            }
        }
    }

    //왼쪽으로 맟출건지 오른쪽으로 맟출건지 결정
    public void AngleCheck()
    {
        switch(angleXYZ)
        {
            case AngleXYZ.X:
                AngleCheck(transform.localRotation.eulerAngles.x);
                break;
            case AngleXYZ.Y:
                AngleCheck(transform.localRotation.eulerAngles.y);
                break;
            case AngleXYZ.Z:
                AngleCheck(transform.localRotation.eulerAngles.z);
                break;
        }
    }
    void AngleCheck(float ang)
    {
        for (int i = 0; i < cutAngle; i++)
        {
            if (i == cutAngle - 1)
            {
                if (ang > centerAngle[i] || ang < centerAngle[0])
                {
                    myPoint = 0;
                }
            }
            else
            {
                if (ang > centerAngle[i] && ang < centerAngle[i + 1])
                {
                    myPoint = i + 1;
                }
            }
        }
    }
   
    //회전
    public void Rotate(float direction)
    {
        transform.Rotate(rotationAngleXYZ * rotateSpeed * direction * Time.deltaTime);
    }

    //최대값 최소값 지정
    void AngleLimit()
    {
        switch(angleXYZ)
        {
            case AngleXYZ.X:
                if(transform.localRotation.eulerAngles.x > 360)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x - 360, 
                        transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
                }
                if (transform.localRotation.eulerAngles.x < 0)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + 360,
                        transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
                }
                break;
            case AngleXYZ.Y:
                if (transform.localRotation.eulerAngles.y > 360)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                        transform.localRotation.eulerAngles.y - 360, transform.localRotation.eulerAngles.z);
                }
                if (transform.localRotation.eulerAngles.y < 0)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                        transform.localRotation.eulerAngles.y + 360, transform.localRotation.eulerAngles.z);
                }
                break;
            case AngleXYZ.Z:
                if (transform.localRotation.eulerAngles.z > 360)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                        transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z - 360);
                }
                if (transform.localRotation.eulerAngles.z < 0)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                        transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z + 360);
                }
                break;
        }
    }

    public void GetPotatoChild()
    {
        foreach(GameObject pot in potato)
        {
            foreach(int num in potatoNumber)
            {
                if(pot.GetComponent<Potato>().myNum == num)
                {
                    pot.transform.parent = transform;
                }
            }
        }
    }

    public void SetPotatoParent()
    {
        for(int i = 1; i <= potatoNumber.Length; i++)
        {
            transform.GetChild(1).GetComponent<Potato>().PositionReset();
            transform.GetChild(1).SetParent(potatoBasket.transform);
        }
    }
}
