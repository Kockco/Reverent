using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPuzzleHandle : MonoBehaviour
{
    public bool isCatch;
    //세 개의 라인 받기
    public PlanetLine[] planetLine;
    public PlanetSecondLine planetSecondLine;
    
    void Update()
    {
        if (isCatch == true)
        {
            foreach (PlanetLine line in planetLine)
            {
                     line.PlanetRotate(Input.GetAxis("Horizontal"));
            }
            planetSecondLine.PlanetRotate(Input.GetAxis("Horizontal"));
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            CatchCheck();
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
    public void HandleRotate(float rotateSpeed,float direction)
    {
        transform.Rotate(0, rotateSpeed * direction * Time.deltaTime, 0);

        foreach (PlanetLine line in planetLine)
                line.PlanetRotate(direction);
    }
}
