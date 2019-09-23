using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMaterialInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Material mat;
        mat = Resources.Load("/Nature/Main_Objects/3.SNOW/StarPuzzlePlatfrorm/StarPuzzleStone", typeof(Material)) as Material;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
