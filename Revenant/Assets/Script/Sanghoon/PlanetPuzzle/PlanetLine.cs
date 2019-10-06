using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLine : MonoBehaviour
{
    //핸들과 연결
    public PlantPuzzleHandle handle;
    public float rotateSpeed;

    public float[] stopAngle;
    public int cutAngle;
    void Start()
    {
        //멈추는 지점
        stopAngle = new float[cutAngle];

        //현재 y로테이션 값에서  360 나눠 구하기
        float startAngle = transform.eulerAngles.y;
        for (int i = 0; i < cutAngle; i++)
        {
            if ((360 * i) + startAngle < 360)
                stopAngle[i] = (360 * i) + startAngle;
            else
            {
                stopAngle[i] = (360 * i) + startAngle - 360;
            }
        }

    }

    void Update()
    {
        if (handle.isCatch) {

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z), rotateSpeed * 0.005f * Time.deltaTime);
        }
    }

    public void PlanetRotate(float direction)
    {
        transform.Rotate(0,rotateSpeed * direction * Time.deltaTime,0);
    }
}
