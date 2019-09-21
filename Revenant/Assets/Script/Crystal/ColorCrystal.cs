using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCrystal : CrystalState
{
    //클리어가 됬는지 안됬는지 확인
    private bool isClear = false;

    public bool IsClear
    {
        get;
        set;
     }

    private void Start()
    {
        LoadMaterial();
    }
   
}
