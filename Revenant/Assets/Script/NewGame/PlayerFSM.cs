using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    Rigidbody rb;

    float h, v, r;
    // 회전 속도 변수
    public float rotSpeed = 80.0f;

    public Transform model;
    //이동관련
    Vector3 move;
    Transform myTransform;
    [Range(0.1f, 30.0f)]
    public float moveSpeed = 5;
    [Range(0.1f, 30.0f)]
    public float jumpPower = 6f;

    public float mySpeed = 4;

    public Transform cameraTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        model = transform.GetChild(0);
        cameraTransform = Camera.main.transform.parent;
        myTransform = transform;
    }
    private void Update()
    {
        //GetAxis 0~1 GetAxisRaw 0-1
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        r = Input.GetAxisRaw("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        myTransform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전
        myTransform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

    }

    public void MoveCalc(float ratio)
    {
    }

}
