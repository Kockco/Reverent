using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momi_Handle : MomiFSMState
{
    new CameraScript cam;
    new AimControll aim;
    GameObject momi;
    public AudioSource[] knob;
    public GameObject col;

    public int handleNum;
    float handleTime;

    bool isParent, isRotate;

    public override void BeginState()
    {
        base.BeginState();
        knob = new AudioSource[2];
        knob[0] = GameObject.Find("Knob1").GetComponent<AudioSource>();
        knob[1] = GameObject.Find("Knob2").GetComponent<AudioSource>();
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
            transform.parent = col.transform.parent;

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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            knob[0].Play();
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            for (int i = 0; i < knob.Length; i++)
            {
                knob[i].Stop();
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            for (int i = 0; i < knob.Length; i++)
            {
                if (knob[i].time > 2.5f)
                {
                    knob[i].Play();
                }
            }
            anime.SetBool("Momi_Push", true);
            HandleRotate();
            RotationVector();
        }
        else
            anime.SetBool("Momi_Push", false);

        if (Input.GetKey(KeyCode.S))
        {
            for (int i = 0; i < knob.Length; i++)
            {
                if (knob[i].time > 2.5f)
                {
                    knob[i].Play();
                }
            }
            anime.SetBool("Momi_Pull", true);
            HandleRotate();
            RotationVector();
        }
        else
            anime.SetBool("Momi_Pull", false);
    }


    void CatchCheck()
    {
        if (transform.parent.parent.tag == "Planet_Handle" || transform.parent.parent.tag == "Planet_Handle2")
        {
            transform.parent.parent.GetComponent<PlanetHandle>().CatchCheck();
        }
        else if (transform.parent.parent.tag == "Star_Handle" || transform.parent.parent.tag == "Star_Handle_2")
        {
            transform.parent.parent.GetComponent<StarHandle>().CatchCheck();
        }
        else if (transform.parent.parent.tag == "Potato_Handle")
        {
            transform.parent.parent.GetComponent<PotatoHandle>().CatchCheck();
        }
    }

    //핸들잡고 돌리는 부분 캐릭터에게
    void HandleRotate()
    {
        if (transform.parent.parent.tag == "Planet_Handle" || transform.parent.parent.tag == "Planet_Handle2")
        {
            transform.parent.parent.GetComponent<PlanetHandle>().HandleRotate(Input.GetAxis("Vertical"));
        }
        else if (transform.parent.parent.tag == "Star_Handle" || transform.parent.parent.tag == "Star_Handle_2")
        {
            transform.parent.parent.GetComponent<StarHandle>().HandleRotate(Input.GetAxis("Vertical"));
        }
        else if (transform.parent.parent.tag == "Potato_Handle")
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
        handleLeftRight[0] = transform.parent.GetChild(0).gameObject;
        handleLeftRight[1] = transform.parent.GetChild(1).gameObject;

        if (Vector3.Distance(transform.position, handleLeftRight[0].transform.position) <
            Vector3.Distance(transform.position, handleLeftRight[1].transform.position))
            transform.position = handleLeftRight[0].transform.position;
        else
            transform.position = handleLeftRight[1].transform.position;

        Debug.Log(handleLeftRight[0].transform.name + ", " + handleLeftRight[1].transform.name);
    }
}