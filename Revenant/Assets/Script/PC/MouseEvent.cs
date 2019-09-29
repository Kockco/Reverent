using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    Player player;
    PlayerAimState aim;
    GameObject cam;

    public float maxAimDistance = 10;

    void Awake()
    {
    }
    void Update()
    {
    }

    void UseStaff()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Crystal");
        // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리, 충돌할 레이어)
        bool isHit = Physics.SphereCast(aim.transform.position, aim.transform.transform.lossyScale.x / 2, aim.transform.transform.forward, out hit, maxAimDistance, layerMask);
        
        if (isHit)
        {
            if (hit.transform.tag == "Empty_Crystal") // 에임과 충돌한것->내스테프와 같은것
            {
                
            }
        }
        //staff.ChangeMaterial();
    }
}
