using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Idle : MomiFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        
    }

    public override void EndState()
    {
        base.EndState();

    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            manager.SetState(MomiState.Move);

        if (Input.GetKeyDown(KeyCode.Space) && isJumped == false)
            JumpMomi();
    }
}
