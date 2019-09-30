using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private Player player;
    void PlayerState.OnEnter(Player player)
    {
        //player 프로퍼티 초기화
        this.player = player;
    }
    void PlayerState.Update()
    {
        if (player.cc.isGrounded)
            player.yVelocity = 0;

        if (player.nowSpeed != 0)
        {
            player.SetState(new PlayerMoveState());
            player.transform.GetChild(0).GetComponent<Animator>().SetBool("move", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
            player.UseHandle();

        player.cc.Move(player.move * Time.deltaTime);
        player.MoveCalc(1f);
        player.Gravity();
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
    }
    
    

}

