using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    public GameObject[] crystal;
    public GameObject[] emptyCrystal;
    public Material[] crystalMaterial;

    private static CrystalManager _instance = null;

    public static CrystalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(CrystalManager)) as CrystalManager;

                if (_instance == null)
                {
                    Debug.LogError("There's no active CrystalManager object");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        crystal = GameObject.FindGameObjectsWithTag("Crystal");
        emptyCrystal = GameObject.FindGameObjectsWithTag("Empty_Crystal");

        int cNum = 0;
        foreach(var i in emptyCrystal)
        {
            i.GetComponent<CrystalState>().myNum = 0;
        }
        foreach (var i in crystal)
        {
            i.GetComponent<CrystalState>().myNum = ++cNum;
        }

        crystalMaterial = new Material[5];
        crystalMaterial[0] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Blue", typeof(Material)) as Material;
        crystalMaterial[1] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_White", typeof(Material)) as Material;
        crystalMaterial[2] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Red", typeof(Material)) as Material;
        crystalMaterial[3] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Black", typeof(Material)) as Material;
        crystalMaterial[4] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Empty", typeof(Material)) as Material;
    }

    public void LoadMaterial(GameObject obj, int matNum, C_STATE stat)
    {
        Material[] mts = new Material[3];
        switch (stat)
        {
            case C_STATE.BLUE:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Blue", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.WHITE:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_White", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.RED:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Red", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.BLACK:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Black", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.EMPTY:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Empty", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
        }
    }
}
