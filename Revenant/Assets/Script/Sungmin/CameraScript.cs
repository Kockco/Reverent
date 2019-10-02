using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject momi;
    public float momiY;

    public float rotationSpeed, scrollSpeed, rotationXMax;
    public float distance, minDis, maxDis;
    float rotX, rotY;

    Vector3 momiPos, momiDirect;

    // Start is called before the first frame update
    void Start()
    {
        momi = GameObject.Find("Momi_Character");
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMomi();
    }

    void LateUpdate()
    {
        transform.LookAt(momiPos);
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
        transform.position = momiPos + momiDirect * -distance;
    }
}
