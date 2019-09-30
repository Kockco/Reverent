using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePotato : MonoBehaviour
{
    public int num;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Potato")
        {
            other.GetComponent<Potato>().myNum = num;
            Debug.Log(other.name);
        }
    }
}
