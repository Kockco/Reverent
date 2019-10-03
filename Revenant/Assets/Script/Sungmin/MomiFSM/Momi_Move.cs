using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Move : MomiFSMState
{
    Transform cameraRot;
    Rigidbody rig;

    public float InputX, InputZ, rotSpeed = 10;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;

    public override void BeginState()
    {
        base.BeginState();

        cameraRot = GameObject.Find("Camera").GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        anime.SetBool("Momi_Move", true);
    }

    public override void EndState()
    {
        base.EndState();

        anime.SetBool("Momi_Move", false);
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            KeyMoveMomi();
        else
            manager.SetState(MomiState.Idle);
    }

    void KeyMoveMomi()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraRot.forward;
        Vector3 right = cameraRot.right;
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
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
