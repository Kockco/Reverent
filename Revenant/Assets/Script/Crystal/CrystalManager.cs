using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    //크리스탈 관리
    public GameObject[] crystal;
    public GameObject[] emptyCrystal;
    //이펙트 관리
    public GameObject[] crystalEffect;

    //마테리얼 관리
    public Material[] crystalMaterial;
    public GameObject[] crystalEffectParticle;

    //싱글턴 생성
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
        crystalEffect = GameObject.FindGameObjectsWithTag("Crystal_Effect");

        int cNum = 0;

        foreach(var i in crystalEffect)
        {
            i.SetActive(false);
        }
        foreach(var i in emptyCrystal)
        {
            i.GetComponent<CrystalState>().myNum = 0;
        }
        foreach (var i in crystal)
        {
            i.GetComponent<CrystalState>().myNum = ++cNum;
        }

        crystalMaterial = new Material[7];
        crystalMaterial[0] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Blue", typeof(Material)) as Material;
        crystalMaterial[1] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Yellow", typeof(Material)) as Material;
        crystalMaterial[2] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Red", typeof(Material)) as Material;
        crystalMaterial[3] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Green", typeof(Material)) as Material;
        crystalMaterial[4] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Empty", typeof(Material)) as Material;
        crystalMaterial[5] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_White", typeof(Material)) as Material;
        crystalMaterial[6] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Purple", typeof(Material)) as Material;
        crystalEffectParticle = new GameObject[18];
        crystalEffectParticle[0] = Resources.Load("FX/1.Revv/CCrystal/Active/CCrystal_Active_Blue 1 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[1] = Resources.Load("FX/1.Revv/CCrystal/Reset/CCrystal_Reset_Blue", typeof(GameObject)) as GameObject;
        crystalEffectParticle[2] = Resources.Load("FX/1.Revv/CCrystal/IN/CC_IN_Blue 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[3] = Resources.Load("FX/1.Revv/CCrystal/Active/CCrystal_Active_Yellow", typeof(GameObject)) as GameObject;
        crystalEffectParticle[4] = Resources.Load("FX/1.Revv/CCrystal/Reset/CCrystal_Reset_Yellow", typeof(GameObject)) as GameObject;
        crystalEffectParticle[5] = Resources.Load("FX/1.Revv/CCrystal/IN/CC_IN_Yellow 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[6] = Resources.Load("FX/1.Revv/CCrystal/Active/CCrystal_Active_Red", typeof(GameObject)) as GameObject;
        crystalEffectParticle[7] = Resources.Load("FX/1.Revv/CCrystal/Reset/CCrystal_Reset_Red", typeof(GameObject)) as GameObject;
        crystalEffectParticle[8] = Resources.Load("FX/1.Revv/CCrystal/IN/CC_IN_Red 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[9] = Resources.Load("FX/1.Revv/CCrystal/Active/CCrystal_Active_Green 1 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[10] = Resources.Load("FX/1.Revv/CCrystal/Reset/CCrystal_Reset_Green", typeof(GameObject)) as GameObject;
        crystalEffectParticle[11] = Resources.Load("FX/1.Revv/CCrystal/IN/CC_IN_Green 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[12] = Resources.Load("FX/1.Revv/CCrystal/Active/CCrystal_Active_White", typeof(GameObject)) as GameObject;
        crystalEffectParticle[13] = Resources.Load("FX/1.Revv/CCrystal/Reset/CCrystal_Reset_White", typeof(GameObject)) as GameObject;
        crystalEffectParticle[14] = Resources.Load("FX/1.Revv/CCrystal/IN/CC_IN_White", typeof(GameObject)) as GameObject;
        crystalEffectParticle[15] = Resources.Load("FX/1.Revv/CCrystal/Active/CCrystal_Active_Purple 1", typeof(GameObject)) as GameObject;
        crystalEffectParticle[16] = Resources.Load("FX/1.Revv/CCrystal/Reset/CCrystal_Reset_Purple", typeof(GameObject)) as GameObject;
        crystalEffectParticle[17] = Resources.Load("FX/1.Revv/CCrystal/IN/CC_IN_Purple", typeof(GameObject)) as GameObject;
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
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_WHITE", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.RED:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Red", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.BLACK:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Green", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
            case C_STATE.EMPTY:
                mts[matNum] = Resources.Load("Nature/Main_Objects/COMMON/CCrystal/Crystal_Empty", typeof(Material)) as Material;
                obj.GetComponent<MeshRenderer>().materials = mts;
                break;
        }
    }
}
