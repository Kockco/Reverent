﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject momi;
    MomiFSMManager momiState;
    public GameObject[] moveToObject;
    public float momiY;

    public float rotationSpeed, rotationXMax, scrollSpeed;
    public float distance, minDis, maxDis;
    public bool isClear;
    float rotX, rotY;

    Vector3 momiPos, momiDirect;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        momi = GameObject.Find("Momi").transform.GetChild(1).gameObject;
        momiState = GameObject.Find("Momi").GetComponent<MomiFSMManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (momiState.CurrentState != MomiState.Handle)
            LookAtMomi();
    }

    void LateUpdate()
    {
        if (momiState.CurrentState != MomiState.Handle)
            transform.LookAt(momiPos);

        RayToWall();
    }

    public void CamMoveToObject(int num)
    {
        Vector3 tempPos = new Vector3(moveToObject[num].transform.position.x, moveToObject[num].transform.position.y, moveToObject[num].transform.position.z);// -(camHeight * 10));
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * 2f);
        transform.rotation = Quaternion.Lerp((Quaternion)transform.rotation, (Quaternion)moveToObject[num].transform.rotation, Time.deltaTime * 2f);
    }

    void LookAtMomi()
    {
        rotX += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -rotationXMax, rotationXMax);
        distance = Mathf.Clamp(distance, minDis, maxDis);

        momiPos = momi.transform.position + Vector3.up * momiY;
        momiDirect = Quaternion.Euler(-rotX, rotY, 0f) * Vector3.forward;

        transform.localPosition = Vector3.Slerp(transform.position, momiPos + momiDirect * -distance, Time.deltaTime * 10);
    }

    void RayToWall()
    {
        Vector3 tempVec = transform.position - momi.transform.position;

        RaycastHit rayHit;
        if (Physics.Raycast(momi.transform.position, tempVec.normalized, out rayHit, distance))
        {
            if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    distance = Vector3.Distance(momi.transform.position, rayHit.point) * 0.8f;
            else
                distance = maxDis;
        }

        if (rayHit.transform != null)
            Debug.Log(rayHit.transform.name + ", RayHitPoint : " + rayHit.point);

        Debug.DrawRay(momi.transform.position, tempVec.normalized * distance, Color.red);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
        }
    }
}