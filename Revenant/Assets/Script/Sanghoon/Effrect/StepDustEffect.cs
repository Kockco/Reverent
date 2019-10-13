using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDustEffect : MonoBehaviour
{
    public GameObject stepDust;
    public float y;
    public float dustSpeed;

    void Start()
    {
        stepDust.transform.position = transform.position;
    }
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        stepDust.transform.position =Vector3.MoveTowards(stepDust.transform.position, pos, dustSpeed * Time.deltaTime);
    }
}
