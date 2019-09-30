using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchingPos : MonoBehaviour
{
    // 카메라가 이동할 좌표를 가진 오브젝트
    [SerializeField] GameObject moveToObject;

    // 오브젝트로부터 얼만큼 멀어질지 (1 이상 권장)
    [SerializeField] float backZ;

    bool camMove;

    // Start is called before the first frame update
    void Start()
    {
        // 이 구문을 통해 오브젝트 교체 가능
        moveToObject = GameObject.Find("CameraPos");    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            camMove = true;

        if (camMove)
        {
            // 위치를 가진 오브젝트로 향할 방향벡터 임시 설정
            Vector3 tempPos = new Vector3(moveToObject.transform.position.x, moveToObject.transform.position.y, -backZ);

            // 상훈이형이 말한 Lerp를 통해 자연스런 이동
            transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * 2f);

            // Quaternion으로 형변환 후에 방향도 자연스럽게 변경
            transform.rotation = Quaternion.Lerp((Quaternion)transform.rotation, (Quaternion)moveToObject.transform.rotation, Time.deltaTime * 2f);

            if (transform.position == tempPos)
                camMove = false;
        }
    }
}