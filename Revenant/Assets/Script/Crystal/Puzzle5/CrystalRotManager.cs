using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotManager : MonoBehaviour
{
    public enum DIRECTION_STATE
    {
        NORMAL,
        RIGHT,
        LEFT,
        END,
        CLEAR
    }
    public bool isActive; //두개다에게 움직일지 말지 판단
    public int activeNum; // 몇번째가 색이들어갔는지 판단
    public GameObject[] Crystal; 
    public DIRECTION_STATE state;

    public int[] clearPoint;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        activeNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == DIRECTION_STATE.NORMAL)
        {
            if (Crystal[0].GetComponent<CrystalRotation>().myPoint == clearPoint[0] && Crystal[1].GetComponent<CrystalRotation>().myPoint == clearPoint[1])
            {
                state = DIRECTION_STATE.END;
            }
            //둘다 비활성화일때만 체크
            if (isActive == false)
            {
                Debug.Log("시작상태!");
                if (Crystal[0].GetComponent<CrystalRotation>().c_state.isActive == false ||
                    Crystal[1].GetComponent<CrystalRotation>().c_state.isActive == false)
                {
                    ActiveCheck();
                }
            }

            //이동이 끝나면 초기화
            if (isActive == true)
            {
                Debug.Log("끝난상태호출");
                if (Crystal[activeNum].GetComponent<CrystalRotation>().state == CrystalRotation.STATE.END ||
                    (Crystal[0].GetComponent<CrystalRotation>().state == CrystalRotation.STATE.NORMAL &&
                    Crystal[1].GetComponent<CrystalRotation>().state == CrystalRotation.STATE.NORMAL))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Crystal[i].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.END;
                        Crystal[i].GetComponent<CrystalRotation>().c_state.isActive = false;
                        activeNum = 0;
                    }
                    isActive = false;
                }
            }
        }
        else if(state == DIRECTION_STATE.END)
        {
            Crystal[0].GetComponent<CrystalRotation>().c_state.tag = "Finish";
            Crystal[1].GetComponent<CrystalRotation>().c_state.tag = "Finish";
            Crystal[0].GetComponent<CrystalRotation>().c_state.myNum = 88;
            Crystal[1].GetComponent<CrystalRotation>().c_state.myNum = 88;
            Crystal[0].GetComponent<CrystalRotation>().c_state.state = C_STATE.EMPTY;
            Crystal[1].GetComponent<CrystalRotation>().c_state.state = C_STATE.EMPTY;
            Crystal[0].GetComponent<CrystalRotation>().c_state.isActive = false;
            Crystal[1].GetComponent<CrystalRotation>().c_state.isActive = false;
            Destroy(Crystal[0].GetComponent<CrystalRotation>());
            Destroy(Crystal[1].GetComponent<CrystalRotation>());
            state = DIRECTION_STATE.CLEAR;
        }
        else if (state == DIRECTION_STATE.CLEAR)
        {
        }

        void ActiveCheck()
        {
            for (int i = 0; i < 2; i++)
            {
                if (Crystal[i].GetComponent<CrystalRotation>().c_state.isActive == true)
                {
                    activeNum = i;
                    if (Crystal[i].GetComponent<CrystalRotation>().c_state.state == C_STATE.BLUE)
                    {
                        Crystal[0].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.RIGHT;
                        Crystal[1].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.RIGHT;
                    }
                    else if (Crystal[i].GetComponent<CrystalRotation>().c_state.state == C_STATE.WHITE)
                    {
                        Crystal[0].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.LEFT;
                        Crystal[1].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.LEFT;
                    }
                    else if (Crystal[i].GetComponent<CrystalRotation>().c_state.state == C_STATE.EMPTY)
                    {
                        Crystal[0].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.RESET;
                        Crystal[1].GetComponent<CrystalRotation>().state = CrystalRotation.STATE.RESET;
                        Crystal[0].GetComponent<CrystalRotation>().c_state.state = C_STATE.EMPTY;
                        Crystal[1].GetComponent<CrystalRotation>().c_state.state = C_STATE.EMPTY;
                    }
                    //다른녀석도 상태를 활성화(항상 동시에 활성화된다)
                    if (i == 0)
                        Crystal[1].GetComponent<CrystalRotation>().c_state.isActive = true;
                    else if (i == 1)
                        Crystal[0].GetComponent<CrystalRotation>().c_state.isActive = true;

                    isActive = true;
                    break;
                }
            }
        }
    }
    
}
