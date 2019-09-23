using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCrystal : CrystalState
{
    public MeshRenderer myMat;
    public bool isActive;

    void Start()
    {
        LoadMaterial();

        myMat = transform.GetChild(0).GetComponent<MeshRenderer>();


        isActive = false;

        if (myNum != 0)
        {
            Debug.Log(transform.name + " number is not 0");
        }
    }
    void Update()
    {
        ChangeMat();
    }
    public void ChangeMat()
    {
        switch (state)
        {
            case C_STATE.BLUE:
                myMat.material = mat[0];
                break;
            case C_STATE.WHITE:
                myMat.material = mat[1];
                break;
            case C_STATE.RED:
                myMat.material = mat[2];
                break;
            case C_STATE.BLACK:
                myMat.material = mat[3];
                break;
            case C_STATE.EMPTY:
                myMat.material = mat[4];
                break;
            case C_STATE.LIGHT:
                myMat.material = mat[5];
                break;
            case C_STATE.DARK:
                myMat.material = mat[6];
                break;
        }
    }
}
