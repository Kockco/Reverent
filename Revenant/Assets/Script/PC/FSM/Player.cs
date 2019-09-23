﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //캐릭터 상태
    public PlayerState currentState;
    public bool useStaff;

    //이동관련
    public Vector3 move;
    public Transform myTransform;
    [Range(0.1f, 30.0f)]
    public float runSpeed = 5;
    [Range(0.1f, 30.0f)]
    public float jumpPower = 6f;

    //중력
    public float gravity = 9.81f;
    public float yVelocity = 0;
    public float rotSpeed = 10f;

    //카메라 관련
    public Transform model;
    public Transform cameraTransform;
    public CharacterController cc;
    
    public float nowSpeed;
    public float staffTime;
    private void Awake()
    {
        //상태변경
        SetState(new PlayerIdleState());
        cc = GetComponent<CharacterController>();
        model = transform.GetChild(0);
        cameraTransform = Camera.main.transform.parent;
        myTransform = transform;
        useStaff = true;
    }

    private void Update()
    {
        //currentState 업데이트 돌리기
        currentState.Update();
        // 현재 움직이는 속도
        nowSpeed = new Vector3(cc.velocity.x, 0, cc.velocity.z).magnitude;
        //if (move.y < -0.5f)
            cc.Move(move * Time.deltaTime);
    }

    public void SetState(PlayerState nextState)
    {
        //other state change
        if (currentState != null)
        {
            currentState.OnExit();
        }
        // next state start
        currentState = nextState;
        currentState.OnEnter(this);
    }

    public void MoveCalc(float ratio)
    {
        float tempMoveY = move.y;
        move.y = 0; //이동에는 xz값만 필요하므로 잠시 저장하고 빼둔다.
        Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //대각선 이동이 루트2 배의 속도를 갖는 것을 막기위해 속도가 1 이상 된다면 노말라이즈 후 속도를 곱해 어느 방향이든 항상 일정한 속도가 되게 한다.
        float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude; //sqrMagnitude연산을 두 번 할 필요 없도록 따로 저장
        inputMoveXZ = myTransform.TransformDirection(inputMoveXZ);

        if (inputMoveXZMgnitude <= 1)
            inputMoveXZ *= runSpeed;
        else
            inputMoveXZ = inputMoveXZ.normalized * runSpeed;

        //조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록 한다.
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraTransform.rotation;
            cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.
            //자연스러움을 위해 Slerp로 회전시킨다.
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, cameraRotation, rotSpeed * Time.deltaTime);
            if (move != Vector3.zero)//Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                model.rotation = Quaternion.Slerp(model.rotation, characterRotation, rotSpeed * Time.deltaTime);
            }

            //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
            move = Vector3.MoveTowards(move, inputMoveXZ, ratio * runSpeed);
        }
        else
        {
            //조작이 없으면 서서히 멈춘다.
            move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * runSpeed * ratio);
        }
        float speed = move.sqrMagnitude;    //현재 속도를 애니메이터에 세팅한다.

        move.y = tempMoveY; //y값 복구
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Jump",true);
            yVelocity = jumpPower;
            SetState(new PlayerJumpState());
        }
    }

    public void MoveJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("MoveJump", true);
            yVelocity = jumpPower;
            SetState(new PlayerMoveJumpState());
        }
    }
    public void Gravity()
    {
        move.y = yVelocity;

        if (yVelocity >= 0)
            yVelocity -= gravity *1 * Time.deltaTime;
        else if (yVelocity > -19 && yVelocity < 0)
            yVelocity -= gravity *2 * Time.deltaTime;
    }
    
    public void PlayerAnimation(string aniName) { model.GetComponent<Animator>().SetTrigger(aniName); }
    public void PlayerAnimation(string aniName,bool b) { model.GetComponent<Animator>().SetBool(aniName,b); }
    public void PlayerAnimation(string aniName, float f) { model.GetComponent<Animator>().SetFloat(aniName, f); }
    //플레이어 애니메이션 실행 함수
    public void PlayerClickAnimation(C_STATE c_state)
    {
        switch (c_state)
        {
            case C_STATE.BLUE:
                PlayerAnimation("Blue");
                break;
            case C_STATE.WHITE:
                PlayerAnimation("Yellow");
                break;
            case C_STATE.RED:
                PlayerAnimation("Red");
                break;
            case C_STATE.BLACK:
                PlayerAnimation("Green");
                break;
            case C_STATE.EMPTY:
                break;
        }
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리, 충돌할 레이어)
        bool isHit = Physics.SphereCast(transform.position, transform.lossyScale.x / 3, -transform.up, out hit, 0.1f);

        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, (-transform.up) * hit.distance);
            Gizmos.DrawWireSphere(transform.position + (-transform.up) * hit.distance, 0.1f);
        }
        else
        {
            Gizmos.DrawRay(transform.position, (-transform.up) * 0.1f);
            //Gizmos.DrawWireSphere(transform.position + (-transform.up) * hit.distance, 0.01f);
        }
    }
}