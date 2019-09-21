﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    Player player;
    PlayerStaff staff;
    PlayerAimState aim;
    GameObject cam;
    public GameObject[] emptyCrystal;
    public int emptyCrystalLenght;
    public float maxAimDistance = 10;
    void Awake()
    {
        player = GameObject.Find("PC").GetComponent<Player>();
        aim = GameObject.Find("Aim").GetComponent<PlayerAimState>();
        staff = GameObject.Find("Staff").GetComponent<PlayerStaff>();
        cam = GameObject.Find("Camera");
        if (player == null)
            Debug.Log("MouseEvent Error : player not find");
        if (aim == null)
            Debug.Log("MouseEvent Error : aim not find");
        if (staff == null)
            Debug.Log("PlayerScript Error : staff not find");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 크리스탈 색 빼기
        {
            player.GetComponent<Player>().PlayerAnimation("Click");
            UseStaff();
            //카메라 돌리는 과정 수정예정
            //Quaternion cameraRotation = cam.transform.parent.rotation;
            //cameraRotation.x = cameraRotation.z = 0;

            //player.transform.GetChild(0).rotation = Quaternion.Slerp(player.transform.GetChild(0).rotation, cameraRotation, 100.0f * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(1))//범위 크리스탈 색 Empty로 바꾸기
        {
            PlayerCrystalReset(true);
        }
    }

    void UseStaff()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Crystal");
        // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리, 충돌할 레이어)
        bool isHit = Physics.SphereCast(aim.transform.position, aim.transform.transform.lossyScale.x / 2, aim.transform.transform.forward, out hit, maxAimDistance, layerMask);
        
        Gizmos.color = Color.red;
        if (isHit)
        {
            if (hit.transform.tag == "Empty_Crystal") // 에임과 충돌한것->내스테프와 같은것
            {
                EmptyCrystal hitCrystal = hit.transform.GetComponent<EmptyCrystal>();

                if (!hitCrystal.isActive && !hitCrystal.isLink
                    && staff.State != C_STATE.EMPTY) //크리스탈 비활성화/링크가 되있지 않을때(엠티)/스태프가 색이 있다면 크리스탈만 변경
                {
                    //링크 해제
                    foreach (var i in CrystalManager.Instance.crystal)
                    {
                        if (i.GetComponent<CrystalState>().myNum == staff.CrystalNum)
                            i.GetComponent<CrystalState>().isLink = true;
                    }
                    //크리스탈만 색을 넣고 스태프는 기본으로 초기화
                    hitCrystal.isActive = true;
                    hitCrystal.isLink = true;
                    hitCrystal.myNum = staff.CrystalNum; //저장되있던 스태프와 Link되있는 넘버정보를 넘김
                    hitCrystal.state = staff.State; // 저장되있던 스태프 상태를 넘김(크리스탈색바뀜)
                    PlayerCrystalReset();
                }
                else if (!hitCrystal.isActive && hitCrystal.isLink) //크리스탈 비활성화/링크가 되있을때는 링크가 된(색이있음) 녀석을 찾아서 풀고 변경
                {
                    //링크 해제
                    foreach (var i in CrystalManager.Instance.crystal)
                    {
                        if (i.GetComponent<CrystalState>().myNum == hitCrystal.myNum)
                            i.GetComponent<CrystalState>().isLink = false;
                        if (i.GetComponent<CrystalState>().myNum == staff.CrystalNum)
                            i.GetComponent<CrystalState>().isLink = true;
                    }
                    //마테리얼을 서로 교체해줌
                    hitCrystal.isActive = true;
                    if (staff.State == C_STATE.EMPTY) //크리스탈에서 스태프로 색만빼기
                    {
                        staff.CrystalNum = hitCrystal.myNum; //빈크리스탈과 Link되잇는 넘버정보넘김
                        staff.State = hitCrystal.state; //빈크리스탈의 상태를 스태프에게 전달(마테리얼도 변경됨)
                        hitCrystal.isLink = false;
                        hitCrystal.myNum = 88; //저장되있던 스태프와 Link되있는 넘버정보를 넘김
                        hitCrystal.state = C_STATE.EMPTY; // 저장되있던 스태프 상태를 넘김(크리스탈색바뀜)
                    }
                    else//크리스탈 색없애고 스태프의 크리스탈 색 넣기
                    {
                        hitCrystal.isLink = true;
                        hitCrystal.myNum = staff.CrystalNum; //저장되있던 스태프와 Link되있는 넘버정보를 넘김
                        hitCrystal.state = staff.State; // 저장되있던 스태프 상태를 넘김(크리스탈색바뀜)
                        PlayerCrystalReset();
                    }
                }
            }
            else if (hit.transform.tag == "Crystal")
            {
                ColorCrystal hitCrystal = hit.transform.GetComponent<ColorCrystal>();
                if (!hitCrystal.isLink && !hitCrystal.IsClear)
                {
                    //완전체 크리스탈의 정보를 스태프로 가져옴
                    staff.CrystalNum = hitCrystal.myNum;
                    staff.State = hitCrystal.state;
                }
            }
            else if (hit.transform.tag == "Wall")
            {
            }
        }
    }
    void PlayerCrystalReset(bool animationPlay = false)
    {
        if(animationPlay)
            player.PlayerAnimation("RClick");

        staff.CrystalNum=88;
        staff.State = C_STATE.EMPTY;
    }
}
