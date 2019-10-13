using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Jump : MomiFSMState
{
    public override void BeginState()
    {
        base.BeginState();

        // anime.SetInteger("Momi_Jump", 1);
        anime.SetTrigger("Momi_Jump");

        Invoke("EndJump", 0.775f);
    }

    public override void EndState()
    {
        base.EndState();
        
    }

    protected override void Update()
    {
        base.Update();

        // if (isGround) manager.SetState(MomiState.Idle);
    }

    void EndJump()
    {
        // anime.SetBool("Momi_Jump", false);
        manager.SetState(MomiState.Idle);
    }
}
