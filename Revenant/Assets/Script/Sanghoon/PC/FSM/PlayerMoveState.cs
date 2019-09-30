using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Player player;
    void PlayerState.OnEnter(Player player)
    {
        //player 프로퍼티 초기화
        this.player = player;
    }
    void PlayerState.Update()
    {
        Debug.Log("move");
        if (player.cc.isGrounded)
        {
            player.yVelocity = 0;
        }
        if (player.nowSpeed ==0)
        {
            player.SetState(new PlayerIdleState());
            player.transform.GetChild(0).GetComponent<Animator>().SetBool("move", false);
        }
        player.MoveCalc(1.0f);
        player.cc.Move(player.move * Time.deltaTime);
        player.Gravity();
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
    }

}
