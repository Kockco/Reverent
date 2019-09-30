using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandle : MonoBehaviour
{
    public PuzzlePlate link_plate;
    public bool isCatch;

    private void Start()
    {
        transform.rotation = link_plate.transform.rotation;
    }
    private void Update()
    {
        if(isCatch)
        {
            link_plate.gameObject.transform.rotation = transform.rotation;
        }
    }
    public void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
        }
        else
            isCatch = false;
    }
}
