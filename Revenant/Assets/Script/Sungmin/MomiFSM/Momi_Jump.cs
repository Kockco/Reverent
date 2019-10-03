using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Jump : MomiFSMState
{
    public override void BeginState()
    {
        base.BeginState();

        // anime.SetInteger("Momi_Jump", 1);
    }

    public override void EndState()
    {
        base.EndState();

        isJumped = false;
    }

    protected override void Update()
    {
        base.Update();

        EndJump();
    }

    void IsGrounded()
    {
        // anime.SetInteger("Momi_Jump", 2);
    }

    void EndJump()
    {
        manager.SetState(MomiState.Idle);
    }
}
