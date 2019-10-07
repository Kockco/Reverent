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
        if (col.transform.tag == "StartHandle" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 1;
        }

        if (col.transform.tag == "PotatoHandle" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 2;
        }

        if (col.transform.tag == "PlanetHandle" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 3;
        }
    }
}