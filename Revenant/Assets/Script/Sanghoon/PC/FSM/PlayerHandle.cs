using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandle : PlayerState
{
    private Player player;
    Vector3 basePos;
    void PlayerState.OnEnter(Player player)
    {
        //player 프로퍼티 초기화
        this.player = player;
        basePos = player.transform.localPosition;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.cc.enabled = false;
    //    player.MoveCalc(0);
    }
    void PlayerState.Update()
    {
        // player.camPlayer.CamMoveToObject(); <- 이거 활성화하면 카메라가 전체를 비춰요!
        player.HandleRotation();
        player.PlayerToRayRotation(player.hit);

        if (player.cc.isGrounded)
        {
           player.yVelocity = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player.transform.parent = null;
            player.cc.enabled = true;
            player.SetState(new PlayerIdleState());
            player.transform.GetChild(0).GetComponent<Animator>().SetBool("move", false);
        }

    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
    }
}
