using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Handle : MomiFSMState
{
    new CameraScript cam;
    new AimControll aim;
    public GameObject col;

    public override void BeginState()
    {
        base.BeginState();

        cam = GameObject.Find("Camera").GetComponent<CameraScript>();
        aim = transform.GetChild(1).GetComponent<AimControll>();
        
    }

    public override void EndState()
    {
        base.EndState();
        
        transform.parent = null;

        anime.SetBool("Momi_Pull", false);
        anime.SetBool("Momi_Push", false);
    }

    protected override void Update()
    {
        base.Update();

        cam.CamMoveToObject();

        RotationMomi();
        RotationHandle();

        // if (Input.GetKeyDown(KeyCode.E)) manager.SetState(MomiState.Idle);
    }

    void RotationMomi()
    {
        transform.parent = col.transform.parent.transform.parent;

        Vector3 tempCol = col.transform.position; tempCol.y = 0;
        Vector3 tempMomi = transform.position; tempMomi.y = 0;

        Vector3 tempVec = (tempCol - tempMomi).normalized;

        transform.rotation = Quaternion.LookRotation(tempVec);
    }

    void RotationHandle()
    {
        Vector3 inputMoveY = new Vector3(0, Input.GetAxis("Vertical") * 100, 0);

        if (Input.GetKey(KeyCode.W))
            anime.SetBool("Momi_Push", true);
        else
            anime.SetBool("Momi_Push", false);

        if (Input.GetKey(KeyCode.S))
            anime.SetBool("Momi_Pull", true);
        else
            anime.SetBool("Momi_Pull", false);

        transform.parent.Rotate(inputMoveY * Time.deltaTime, Space.Self);
    }
}
