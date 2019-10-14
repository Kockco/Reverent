using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLine : MonoBehaviour
{
    //핸들,각도,속도
    #region
    [Header("행성번호")]
    public int planetNumber;

    [Header("자식")]
    [SerializeField]
    GameObject planet;

    [Header("연결될 핸들")]
    [SerializeField]
    PlanetHandle handle;

    [Header("각도의 갯수")]
    public  int cutAngle;

    [Header("나의 지점")]
    public int myPoint;
    [Header("돌아야하는 각(x,y,z) 한개만 1")]
    [SerializeField]
    Vector3 rotationAngleXYZ;

    [Header("자전속도")]
    [SerializeField]
    float selfRotateSpeed;

    [Header("겹쳐지는 라인 갯수,지점")]
    public int[] overlapLine;

    [Header("현재 라인에 행성이 존재하는가?")]
    public bool isPlanet;
    #endregion


    //속도, 각, 중간 각, 움직이는중인지, 어느각으로 도는지?
    #region
    [HideInInspector]
    public float rotateSpeed;

    [HideInInspector]
    public float returnSpeed;

    [HideInInspector]
    public float[] stopAngle;

    [HideInInspector]
    public float[] centerAngle;

    [HideInInspector]
    public bool isLock;

    enum AngleXYZ { X, Y, Z }
    AngleXYZ angleXYZ;
    #endregion

    void Start()
    {
        stopAngle = new float[cutAngle];
        centerAngle = new float[cutAngle];
        isLock = true;
        //행성이 있으면 true 없으면 false
        if (transform.childCount == 0) isPlanet = false;
        else isPlanet = true;

        //360 / 잘린갯수만큼 계산
        float startAngle = 360 / cutAngle;
        for (int i = 0; i < cutAngle; i++)
        {
            stopAngle[i] = i * startAngle;
            centerAngle[i] = (startAngle / 2) + startAngle * i;
        }

        //시작할때 지점
        Quaternion startingAngleXYZ = Quaternion.Euler(rotationAngleXYZ * stopAngle[myPoint] + transform.eulerAngles);
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

    //왼쪽으로 맟출건지 오른쪽으로 맟출건지 결정
    public void AngleCheck()
    {
        switch (angleXYZ)
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
        ChildRotation(direction);
    }

    //자전
    void ChildRotation(float direction)
    {
         planet.transform.Rotate(rotationAngleXYZ * selfRotateSpeed * direction * Time.deltaTime);
    }

    //최대값 최소값 지정
    void AngleLimit()
    {
        switch (angleXYZ)
        {
            case AngleXYZ.X:
                if (transform.localRotation.eulerAngles.x > 360)
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

    //자동으로 움직이기
    void AutoRotation()
    {
        if (!handle.isCatch)
        {
            if (!isLock)
            {
                Debug.Log(name + myPoint);
                switch (angleXYZ)
                {
                    case AngleXYZ.X:
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(stopAngle[myPoint],
                            transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z), returnSpeed * Time.deltaTime);
                        if ((int)transform.localRotation.eulerAngles.x == stopAngle[myPoint])
                        {
                            isLock = true;
                            if (myPoint == 0 && cutAngle > 3)
                                GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PlanetPuzzleClearCheck(planetNumber);
                        }
                        break;
                    case AngleXYZ.Y:
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
                            stopAngle[myPoint], transform.localRotation.eulerAngles.z), returnSpeed * Time.deltaTime);
                        if ((int)transform.localRotation.eulerAngles.y == stopAngle[myPoint])
                        {
                            isLock = true;
                            if (myPoint == 0 && cutAngle > 3)
                                GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PlanetPuzzleClearCheck(planetNumber);

                        }
                        break;
                    case AngleXYZ.Z:
                        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x,
                            transform.localRotation.eulerAngles.y, stopAngle[myPoint]), returnSpeed * Time.deltaTime);
                        if ((int)transform.localRotation.eulerAngles.z == stopAngle[myPoint])
                        {
                            isLock = true;
                            if (myPoint == 0 && cutAngle > 3)
                                GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PlanetPuzzleClearCheck(planetNumber);

                        }
                        break;
                }
            }
        }
    }

    public void GetPlanet(PlanetLine other)
    {
        if(transform.childCount == 0)
        {
            for(int i =0; i < other.overlapLine.Length; i++)
            {
                if(other.overlapLine[i] == other.myPoint)
                {
                    myPoint = overlapLine[i];
                    transform.localRotation = Quaternion.Euler(rotationAngleXYZ * stopAngle[overlapLine[i]]);
                    other.isPlanet = false;
                    isPlanet = true;
                    planet.transform.parent.transform.SetParent(this.transform);
                }
            }
        }
    }
}
