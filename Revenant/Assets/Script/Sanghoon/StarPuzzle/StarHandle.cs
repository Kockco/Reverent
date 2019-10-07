using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarHandle : MonoBehaviour
{
    public bool isCatch;
    //세 개의 라인 받기
    public StarPlate starPlate;
    
    public void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
            starPlate.isLock = false;
        }
        else
        {
            isCatch = false;
            //포인트 제자리로 돌리기
            starPlate.AngleCheck();
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    public void HandleRotate(float direction)
    {
        starPlate.StarRotate(direction);
    }
}
