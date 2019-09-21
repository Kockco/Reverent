using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private Player player;
    float delay;
    bool groundCheck;
    void PlayerState.OnEnter(Player player)
    {
        //player 프로퍼티 초기화
        this.player = player;
        // 초기화 구현
        delay = 0;
        groundCheck = false;
    }
    void PlayerState.Update()
    {
        if (!groundCheck)
        {
            delay += Time.deltaTime;
            if (delay > 0.25f)
                groundCheck = true;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, Vector3.down, out hit) && hit.distance < 0.15f)
            {
                player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("JumpEnd");
                player.transform.GetChild(0).GetComponent<Animator>().SetBool("Jump", false);
                player.nextDelay = 0;
                player.SetState(new PlayerIdleState());
            }
        }

        player.MoveCalc(0.3f);
        player.Gravity();
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
        delay = 0;
        groundCheck = false;
    }
}
