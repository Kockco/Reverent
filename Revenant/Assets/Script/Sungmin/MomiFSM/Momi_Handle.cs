using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Handle : MomiFSMState
{
    new CameraScript cam;
    new AimControll aim;
    GameObject momi;
    public GameObject col;

    public int handleNum;
    float handleTime;

    bool isParent, isRotate;

    public override void BeginState()
    {
        base.BeginState();

        cam = GameObject.Find("Camera").GetComponent<CameraScript>();
        aim = transform.GetChild(1).GetComponent<AimControll>();
        momi = this.transform.gameObject;

        isParent = false;
    }

    public override void EndState()
    {
        CatchCheck();
        base.EndState();

        transform.parent = null;
        handleTime = 0;

        anime.SetBool("Momi_Pull", false);
        anime.SetBool("Momi_Push", false);

        isParent = false;
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
                SetMomiPosToHandle();
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
        if (Input.GetKey(KeyCode.W))
        {
            anime.SetBool("Momi_Push", true);
            HandleRotate();
            RotationVector();
        }
        else
            anime.SetBool("Momi_Push", false);

        if (Input.GetKey(KeyCode.S))
        {
            anime.SetBool("Momi_Pull", true);
            HandleRotate();
            RotationVector();
        }
        else
            anime.SetBool("Momi_Pull", false);
    }

    void CatchCheck()
    {
        if (transform.parent.tag == "Planet_Handle")
        {
            transform.parent.GetComponent<PlantPuzzleHandle>().CatchCheck();
        }
        else if (transform.parent.tag == "Planet_Star")
        {
            transform.parent.GetComponent<StarHandle>().CatchCheck();
        }
        else if (transform.parent.tag == "Potato_Handle")
        {
            transform.parent.GetComponent<PotatoHandle>().CatchCheck();
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
        else if (transform.parent.tag == "Potato_Handle")
        {
            transform.parent.GetComponent<PotatoHandle>().HandleRotate(Input.GetAxis("Vertical"));
        }
    }

    void RotationVector()
    {
        Vector3 inputMoveY = new Vector3(0, Input.GetAxis("Vertical") * 50, 0);

        if (momi.transform.localEulerAngles.y >= 180)
            inputMoveY = -inputMoveY;

        transform.parent.Rotate(inputMoveY * Time.deltaTime);
    }

    void SetMomiPosToHandle()
    {
        GameObject[] handleLeftRight;
        handleLeftRight = new GameObject[2];
        handleLeftRight[0] = transform.parent.GetChild(1).gameObject;
        handleLeftRight[1] = transform.parent.GetChild(2).gameObject;

        if (Vector3.Distance(transform.position, handleLeftRight[0].transform.position) <
            Vector3.Distance(transform.position, handleLeftRight[1].transform.position))
            transform.position = handleLeftRight[0].transform.position;
        else
            transform.position = handleLeftRight[1].transform.position;

        Debug.Log(handleLeftRight[0].transform.name + ", " + handleLeftRight[1].transform.name);
    }
}