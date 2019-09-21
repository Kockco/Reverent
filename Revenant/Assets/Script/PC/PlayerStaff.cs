using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    int crystalNum;
    public int CrystalNum
    {
        get;
        set;
    }

    Material mat;
    public Material[] changeMat;
    C_STATE state;
    public C_STATE State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            ChangeMaterial();
        }
    }

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        changeMat = new Material[5];
        changeMat[0] = Resources.Load("Nature/Main_Objects/COMMON/Girl/M_Staff_Blue", typeof(Material)) as Material;
        changeMat[1] = Resources.Load("Nature/Main_Objects/COMMON/Girl/M_Staff_White", typeof(Material)) as Material;
        changeMat[2] = Resources.Load("Nature/Main_Objects/COMMON/Girl/M_Staff_Red", typeof(Material)) as Material;
        changeMat[3] = Resources.Load("Nature/Main_Objects/COMMON/Girl/M_Staff_Black", typeof(Material)) as Material;
        changeMat[4] = Resources.Load("Nature/Main_Objects/COMMON/Girl/M_Staff", typeof(Material)) as Material;

        state = C_STATE.EMPTY;
        crystalNum = 88;

        ChangeMaterial();
    }
    
    public void ChangeMaterial()
    {
        switch (state)
        {
            case C_STATE.BLUE:
                mat = changeMat[0];
                break;
            case C_STATE.WHITE:
                mat = changeMat[1];
                break;
            case C_STATE.RED:
                mat = changeMat[2];
                break;
            case C_STATE.BLACK:
                mat = changeMat[3];
                break;
            case C_STATE.EMPTY:
                mat = changeMat[4];
                break;
        }
    }
}
