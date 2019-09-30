using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tq : MonoBehaviour
{
    public GameObject tg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = tg.transform.rotation;








    }
}
