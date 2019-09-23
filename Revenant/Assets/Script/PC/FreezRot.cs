using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezRot : MonoBehaviour
{
    public GameObject direction;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = direction.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        direction.transform.rotation = Quaternion.Euler(rot.x + 90, rot.y + 90, rot.z);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(direction.activeSelf == false)
                direction.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (direction.activeSelf == true)
                direction.SetActive(false);
        }
    }
}

