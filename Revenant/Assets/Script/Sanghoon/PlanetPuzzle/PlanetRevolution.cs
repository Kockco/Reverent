using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRevolution : MonoBehaviour
{
    //그냥 항상 부모의 한칸에 반을 도는 녀석
    PlanetLine myPlanet;
    public GameObject revolutionPlanet;
    float rotateSpeed;
    
    private void Start()
    {
        myPlanet = GetComponent<PlanetLine>();
    }
    private void Update()
    {
        revolutionPlanet.transform.rotation = Quaternion.Euler(0,(transform.eulerAngles.y * myPlanet.cutAngle/2)+ 90, 0);
    }
    
}
