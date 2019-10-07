using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Handle : MomiFSMState
{
    new CameraScript cam;
    new AimControll aim;
    public GameObject col;

    public int handleNum;
    float handleTime;

    bool isParent;

    public override void BeginState()
    {
        base.BeginState();

        cam = GameObject.Find("Camera").GetComponent<CameraScript>();
        aim = transform.GetChild(1).GetComponent<AimControll>();

        isParent = false;
    }

    public override void EndState()
    {
        base.EndState();

        transform.parent = null;
        handleTime = 0;

        anime.SetBool("Momi_Pull", false);
        anime.SetBool("Momi_Push", false);

        CatchCheck(); isParent = false;
    }

    protected override void Update()
    {
        // base.Update();
        handleTime += Time.deltaTime;

        cam.CamMoveToObject(handleNum);

        RotationMomi();
        RotationHandle();

        if (Input.GetKeyDown(KeyCode.E) && handleTime >= 1f)
            manager.SetState(MomiState.Idle);
    }

    void RotationMomi()
    {
        if (col.transform != null)
        {
            try
            {
                transform.parent = col.transform.parent.transform.parent;
            }
            catch
            {
                transform.parent = col.transform.parent;
            }

            if (!isParent)
            {
                CatchCheck();
                   isParent = true;
            }
        }

        Vector3 tempCol = col.transform.position; tempCol.y = 0;
        Vector3 tempMomi = transform.position; tempMomi.y = 0;

        Vector3 tempVec = (tempCol - tempMomi);

        transform.rotation = Quaternion.LookRotation(tempVec);
    }

    void RotationHandle()
    {
        Vector3 inputMoveY = new Vector3(0, Input.GetAxis("Vertical") * 50, 0);

        if (Input.GetKey(KeyCode.W))
        {
            anime.SetBool("Momi_Push", true);
            transform.parent.Rotate(inputMoveY * Time.deltaTime, Space.Self);
            HandleRotate();
        }
        else
            anime.SetBool("Momi_Push", false);

        if (Input.GetKey(KeyCode.S))
        {
            anime.SetBool("Momi_Pull", true);
            transform.parent.Rotate(inputMoveY * Time.deltaTime, Space.Self);
            HandleRotate();
        }
        else
            anime.SetBool("Momi_Pull", false);
    }

    void CatchCheck()
    {
        if(transform.parent.tag == "Planet_Handle")
        {
            transform.parent.GetComponent<PlantPuzzleHandle>().CatchCheck();
        }
        else if(transform.parent.tag == "Planet_Star")
        {
            transform.parent.GetComponent<StarHandle>().CatchCheck();
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    void HandleRotate()
    {
        if (transform.parent.tag == "Planet_Handle")
        {
            transform.parent.GetComponent<PlantPuzzleHandle>().HandleRotate(Input.GetAxis("Vertical"));
        }
        else if (transform.parent.tag == "Planet_Star")
        {
            transform.parent.GetComponent<StarHandle>().HandleRotate(Input.GetAxis("Vertical"));
        }
    }
}