using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        Vector3 inputMoveXZ = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Debug.Log(rb.velocity);
        if(rb.velocity.x < 7.5f && rb.velocity.x > -7.5f)
            rb.AddForce(new Vector3(inputMoveXZ.normalized.x * 25,0,0));
        if (rb.velocity.z < 7.5f && rb.velocity.z > -7.5f)
            rb.AddForce(new Vector3(0, 0, inputMoveXZ.normalized.z * 25));
        //rb.velocity = inputMoveXZ.normalized * 6;
    }
}
