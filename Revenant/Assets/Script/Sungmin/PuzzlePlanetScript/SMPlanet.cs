using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMPlanet : MonoBehaviour
{
    [SerializeField] GameObject[] spot;
    [SerializeField] int maxSpot;

    int moveCount = 0;
    float moveSpeed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spot = new GameObject[maxSpot];
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected void MoveNextSpot(int temp)
    {
        if (temp > 0)
            transform.rotation = Quaternion.RotateTowards(spot[moveCount].transform.rotation, spot[moveCount++].transform.rotation, moveSpeed);
        else
            transform.rotation = Quaternion.RotateTowards(spot[moveCount].transform.rotation, spot[moveCount--].transform.rotation, moveSpeed);
    }
}
