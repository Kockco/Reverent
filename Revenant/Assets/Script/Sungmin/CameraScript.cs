using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject momi;
    MomiFSMManager momiState;
    public GameObject moveToObject;
    public float momiY;

    public float rotationSpeed, scrollSpeed, rotationXMax;
    public float distance, minDis, maxDis;
    public bool isClear;
    float rotX, rotY;

    Vector3 momiPos, momiDirect;

    // Start is called before the first frame update
    void Start()
    {
        momi = GameObject.Find("Momi_Character");
        momiState = GameObject.Find("Momi").GetComponent<MomiFSMManager>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // if (momiState.CurrentState != MomiState.Handle)

        if (momiState.CurrentState != MomiState.Handle)
        {
            LookAtMomi();
            transform.LookAt(momiPos);
        }
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
        transform.position = Vector3.Slerp(transform.position, momiPos + momiDirect * -distance, Time.deltaTime * 2);
        // transform.position = momiPos + momiDirect * -distance;
    }

    public void CamMoveToObject()
    {
        Vector3 tempPos = new Vector3(moveToObject.transform.position.x, moveToObject.transform.position.y, moveToObject.transform.position.z);// -(camHeight * 10));
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * 2f);
        transform.rotation = Quaternion.Lerp((Quaternion)transform.rotation, (Quaternion)moveToObject.transform.rotation, Time.deltaTime * 2f);
        
    }
}
