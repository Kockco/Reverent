using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseStaffState : PlayerState
{
    private Player player;
    float a;
    
    void PlayerState.OnEnter(Player player)
    {
        player.transform.GetChild(0).GetComponent<Animator>().SetBool("move", false);
        //player 프로퍼티 초기화
        this.player = player;
        player.useStaff = false;
        player.move = Vector3.zero;
        a = 0; 
    }
    void PlayerState.Update()
    {
        a += Time.deltaTime;
        if(a > player.staffTime)
        {
            player.PlayerAnimation("Input", false);
            player.SetState(new PlayerIdleState());

        }
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
        player.useStaff = true;
        player.runSpeed = player.mySpeed;
    }
    
}
