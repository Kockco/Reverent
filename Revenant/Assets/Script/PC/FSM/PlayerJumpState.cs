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
        player.runSpeed = 1;
        player.rotSpeed = 0;
        player.useStaff = false;
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
            int layerMask = 1 << LayerMask.NameToLayer("Crystal");
            // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리, )
            bool isHit = Physics.SphereCast(player.transform.position, player.transform.lossyScale.x / 2, -player.transform.up, out hit, 0.1f);
            if (Physics.Raycast(player.transform.position, Vector3.down, out hit) && hit.distance < 0.15f)
            {
                player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("JumpEnd");
                player.transform.GetChild(0).GetComponent<Animator>().SetBool("Jump", false);
                player.SetState(new PlayerIdleState());
            }
        }
        player.MoveCalc(10f);
        player.Gravity();
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
        delay = 0;
        groundCheck = false;
        player.useStaff = true;
    }
    
}
