using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCrystal : CrystalState
{
    GameObject[] CrystalEffect;
    private void Start()
    {
        LoadMaterial();
        if (name == "Blue")
        {
            CrystalEffect = new GameObject[3];
            CrystalEffect[0] = transform.GetChild(3).gameObject;
            CrystalEffect[1] = transform.GetChild(4).gameObject;
            CrystalEffect[2] = transform.GetChild(5).gameObject;
        }
    }
    void Update()
    {
        if (name == "Blue")
        {
            if (IsLink)
            {
                CrystalEffect[0].SetActive(true);
                CrystalEffect[1].SetActive(false);
            }
            else if (!IsLink)
            {
                CrystalEffect[0].SetActive(false);
                CrystalEffect[1].SetActive(true);

            }
        }
    }
    public void CrystalPopEffect()
    {
        CrystalEffect[2].SetActive(false);
        CrystalEffect[2].SetActive(true);
    }
}
