using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollision : MonoBehaviour
{
    public bool onChange;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        onChange = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //2번째 플레닛의 이름이면
        if(other.tag == "Planet_ChangePoint")
        {
            onChange = true;
            if (other.name == "FirstChangePoint")
            {
                number = 1;
            }
            if (other.name == "SecondChangePoint")
            {
                number = 2;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //2번째 플레닛의 이름이면
        if (other.tag == "Planet_ChangePoint")
        {
            onChange = false;
        }
    }
}
