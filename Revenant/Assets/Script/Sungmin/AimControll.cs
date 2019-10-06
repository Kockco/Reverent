using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControll : MonoBehaviour
{
    MomiFSMManager momiManager;
    Momi_Handle momiHandle;
    
    // Start is called before the first frame update
    void Start()
    {
        momiManager = GameObject.Find("Momi").GetComponent<MomiFSMManager>();
    }

    // Update is called once per frame
    // void Update() {    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.tag == "Handle" && Input.GetKeyDown(KeyCode.E))
        {
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
            momiHandle.col = col.gameObject;
        }
    }
}
