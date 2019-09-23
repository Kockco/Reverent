using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCrystal : CrystalState
{
    public GameObject[] CrystalEffect;
    private void Start()
    {
        GameObject[] obj = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            obj[i] = Instantiate(CrystalManager.Instance.crystalEffectParticle[i], Vector3.zero, Quaternion.identity) as GameObject;
            obj[i].transform.SetParent(transform);
            obj[i].transform.localPosition = Vector3.zero;
            obj[i].transform.localRotation = Quaternion.identity;

        }

        LoadMaterial();
        CrystalEffect = new GameObject[3];
        CrystalEffect[0] = transform.GetChild(3).gameObject;
        CrystalEffect[1] = transform.GetChild(4).gameObject;
        CrystalEffect[2] = transform.GetChild(5).gameObject;
    }
    void Update()
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
    public void CrystalPopEffect()
    {
        CrystalEffect[2].SetActive(false);
        CrystalEffect[2].SetActive(true);
    }
}
