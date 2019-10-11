using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHandle : MonoBehaviour
{
    [HideInInspector]
    public bool isCatch;

    [Header("회전속도")]
    [SerializeField]
    float allPlanetRotateSpeed;

    [Header("제자리로 오는 속도")]
    [SerializeField]
    float allPlanetReturnSpeed;

    //세 개의 라인 받기
    public PlanetLine[] planetLine;
    public PlanetSecondLine planetSecondLine;

    private void Awake()
    {
        //스피드 / 잘린지점 = 모든라인 속도 비율이 같음
        foreach (PlanetLine line in planetLine) {
            line.rotateSpeed = allPlanetRotateSpeed / line.CutAngle;
            line.rotateSpeed = allPlanetReturnSpeed / line.CutAngle;
        }
    }

    public void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
            
            foreach (PlanetLine line in planetLine)
                    line.isLock = false;

            if (planetSecondLine.isChild)
                planetSecondLine.isLock = false;
        }
        else
        {
            isCatch = false;
            //포인트 제자리로 돌리기
            foreach (PlanetLine line in planetLine)
                    line.AngleCheck();

            if (planetSecondLine.isChild)
                planetSecondLine.AngleCheck();
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    public void HandleRotate(float direction)
    {
        foreach (PlanetLine line in planetLine)
                line.Rotate(direction);
    }
}
