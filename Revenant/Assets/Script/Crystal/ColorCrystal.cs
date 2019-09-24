using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCrystal : CrystalState
{
    public GameObject[] CrystalEffect;
    private void Start()
    {
        GameObject[] obj = new GameObject[4];
        switch (state)
        {
            case C_STATE.LIGHT:
                for (int i = 12; i < 15; i++)
                {
                    obj[i-12] = Instantiate(CrystalManager.Instance.crystalEffectParticle[i], Vector3.zero, Quaternion.identity) as GameObject;
                    obj[i - 12].transform.SetParent(transform);
                    obj[i - 12].transform.position = new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z);
                    obj[i - 12].transform.rotation = Quaternion.identity;
                }
                break;
            case C_STATE.DARK:
                for (int i = 15; i < 18; i++)
                {
                    obj[i - 15] = Instantiate(CrystalManager.Instance.crystalEffectParticle[i], Vector3.zero, Quaternion.identity) as GameObject;
                    obj[i - 15].transform.SetParent(transform);
                    obj[i - 15].transform.position = new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z);
                    obj[i - 15].transform.rotation = Quaternion.identity;
                }
                break;
        }
        obj[3] = Instantiate(CrystalManager.Instance.crystalEffectParticle[18], Vector3.zero, Quaternion.identity) as GameObject;
        obj[3].transform.SetParent(transform);
        obj[3].transform.position = new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z);
        obj[3].transform.rotation = Quaternion.identity;

        obj[1].transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));

        LoadMaterial();
        CrystalEffect = new GameObject[5];
        CrystalEffect[0] = transform.GetChild(3).gameObject;
        CrystalEffect[1] = transform.GetChild(4).gameObject;
        CrystalEffect[2] = transform.GetChild(5).gameObject;
        CrystalEffect[3] = transform.GetChild(6).gameObject;
    }
    void Update()
    {
        if (IsLink)
        {
            CrystalEffect[0].SetActive(true);
            CrystalEffect[1].SetActive(false);
            CrystalEffect[3].SetActive(true);
        }
        else if (!IsLink)
        {
            CrystalEffect[0].SetActive(false);
            CrystalEffect[1].SetActive(true);
            CrystalEffect[3].SetActive(false);
        }
    }
    public void CrystalPopEffect()
    {
        CrystalEffect[2].SetActive(false);
        CrystalEffect[2].SetActive(true);
    }
}
