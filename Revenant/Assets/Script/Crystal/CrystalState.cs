using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum C_STATE { BLUE, WHITE, RED, BLACK, EMPTY }

public class CrystalState : MonoBehaviour
{
    public int myNum;
    public C_STATE state;
    public Material[] mat; //마테리얼 
    bool isLink = false; //링크가 되어있는지 확인
    public bool IsLink { get; set; }
    bool isClear = false;
    public bool IsClear { get; set; }

    public void LoadMaterial()
    {
        mat = CrystalManager.Instance.crystalMaterial;
    }
    public void Reset()
    {
        state = C_STATE.EMPTY;
        myNum = 88;
    }
}
