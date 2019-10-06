using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Idle : MomiFSMState
{
    public float time;

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
        base.Update();

        RollAround();
    }

    void RollAround()
    {
        time += Time.deltaTime;

        if (time >= 4f)
        {
            time = -2f;
            anime.SetTrigger("Momi_RollAround");
        }

    }
}
