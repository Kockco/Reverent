using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Handle : MomiFSMState
{
    CameraScript cam;

    public override void BeginState()
    {
        base.BeginState();

        cam = GameObject.Find("Camera").GetComponent<CameraScript>();
    }

    public override void EndState()
    {
        base.EndState();

    }

    protected override void Update()
    {
        cam.CamMoveToObject();
    }
}
