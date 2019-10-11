using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoHandle : MonoBehaviour
{
    [HideInInspector]
    public bool isCatch;

    public PotatoPlate potatoPlate;

    void Start()
    {
        isCatch = false;
    }

    public void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
            potatoPlate.isLock = false;
            potatoPlate.GetPotatoChild();
        }
        else
        {
            isCatch = false;
            //포인트 제자리로 돌리기
            potatoPlate.AngleCheck();
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    public void HandleRotate(float direction)
    {
        potatoPlate.Rotate(direction);
    }
}
