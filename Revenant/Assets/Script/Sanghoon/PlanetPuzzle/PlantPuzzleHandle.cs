using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPuzzleHandle : MonoBehaviour
{
    public bool isCatch;

    //세 개의 라인 받기
    public PlanetLine First_Line;
    public PlanetLine Second_Line;
    public PlanetLine Third_Line;

    void Start()
    {
        
    }
    void Update()
    {
        First_Line.PlanetRotate(Input.GetAxis("Horizontal"));
        Second_Line.PlanetRotate(Input.GetAxis("Horizontal"));
        Third_Line.PlanetRotate(Input.GetAxis("Horizontal"));
    }
    public void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
        }
        else
        {
            isCatch = false;
        }
    }
}
