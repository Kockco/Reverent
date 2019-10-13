using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Move : MomiFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        
        // rig = GetComponent<Rigidbody>();
        anime.SetBool("Momi_Move", true);
    }

    public override void EndState()
    {
        base.EndState();

        anime.SetBool("Momi_Move", false);
    }

    protected override void Update()
    {
        if (!Input.anyKey)
            manager.SetState(MomiState.Idle);
    }

    void OnCollisionStay(Collision col)
    {
        switch (col.transform.tag)
        {
            
        }
    }
}
/*
    void CameraFrontViewMomi()
    {
        Quaternion tempQuat = cameraRot.transform.rotation;
        tempQuat.x = tempQuat.z = 0;

        Quaternion charQuat = Quaternion.Lerp(transform.rotation, cameraRot.rotation, Time.deltaTime * 10);
        charQuat.x = charQuat.z = 0;

        transform.rotation = charQuat;
    }
*/
