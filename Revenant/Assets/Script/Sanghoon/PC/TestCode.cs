using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputMoveX = new Vector3(0, Input.GetAxis("Horizontal") * 100, 0);
        transform.parent.Rotate(inputMoveX * Time.deltaTime, Space.Self);
    }
}
