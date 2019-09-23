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

    public int kong = 0;

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
            //ChangeMaterial();
        }
    }

    GameObject[] crystalEffect;

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
        if (crystalNum != 0)
            crystalNum = 0;

        crystalEffect = new GameObject[12];
        int a = 0;
        foreach(var i in crystalEffect)
        {
            crystalEffect[a] = transform.GetChild(0).GetChild(a++).gameObject;
        }
        
        ChangeMaterial();
    }

    private void Update()
    {
        if(kong == 3)
        {
            ChangeMaterial();
        }
    }
    public void ChangeMaterial()
    {
        kong = 0;
        for(int i = 4; i < 7; i++)
        {
            crystalEffect[i].SetActive(false);
        }
        Invoke("IngCrystalEffect", 0.5f);
        switch (state)
        {
            case C_STATE.BLUE:
                GetComponent<MeshRenderer>().material = changeMat[0];
                crystalEffect[0].SetActive(false);
                crystalEffect[0].SetActive(true);
                break;
            case C_STATE.WHITE:
                GetComponent<MeshRenderer>().material = changeMat[1];
                crystalEffect[1].SetActive(false);
                crystalEffect[1].SetActive(true);
                break;
            case C_STATE.RED:
                GetComponent<MeshRenderer>().material = changeMat[2];
                crystalEffect[2].SetActive(false);
                crystalEffect[2].SetActive(true);
                break;
            case C_STATE.BLACK:
                GetComponent<MeshRenderer>().material = changeMat[3];
                crystalEffect[3].SetActive(false);
                crystalEffect[3].SetActive(true);
                break;
            case C_STATE.EMPTY:
                GetComponent<MeshRenderer>().material = changeMat[4];
                break;
            case C_STATE.LIGHT:
                GetComponent<MeshRenderer>().material = changeMat[1];
                crystalEffect[1].SetActive(false);
                crystalEffect[1].SetActive(true);
                break;
            case C_STATE.DARK:
                GetComponent<MeshRenderer>().material = changeMat[3];
                crystalEffect[3].SetActive(false);
                crystalEffect[3].SetActive(true);
                break;
        }
    }
    
    void IngCrystalEffect()
    {
        switch (state)
        {
            case C_STATE.BLUE:
                crystalEffect[4].SetActive(true);
                break;
            case C_STATE.WHITE:
                crystalEffect[5].SetActive(true);
                break;
            case C_STATE.RED:
                crystalEffect[6].SetActive(true);
                break;
            case C_STATE.BLACK:
                crystalEffect[7].SetActive(true);
                break;
            case C_STATE.EMPTY:
                break;
            case C_STATE.LIGHT:
                crystalEffect[5].SetActive(true);
                break;
            case C_STATE.DARK:
                crystalEffect[7].SetActive(true);
                break;
        }
    }
    public void OutCrystalEffect()
    {
        switch (state)
        {
            case C_STATE.BLUE:
                crystalEffect[8].SetActive(false);
                crystalEffect[8].SetActive(true);
                break;
            case C_STATE.WHITE:
                crystalEffect[9].SetActive(false);
                crystalEffect[9].SetActive(true);
                break;
            case C_STATE.RED:
                crystalEffect[10].SetActive(false);
                crystalEffect[10].SetActive(true);
                break;
            case C_STATE.BLACK:
                crystalEffect[11].SetActive(false);
                crystalEffect[11].SetActive(true);
                break;
            case C_STATE.EMPTY:
                break;
            case C_STATE.LIGHT:
                crystalEffect[9].SetActive(false);
                crystalEffect[9].SetActive(true);
                break;
            case C_STATE.DARK:
                crystalEffect[11].SetActive(false);
                crystalEffect[11].SetActive(true);
                break;
        }
    }
}
