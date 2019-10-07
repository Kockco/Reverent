using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    Transform player;
    Transform cam;
    Quaternion rot;

    public float maxAimDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PC").transform;
        cam = GameObject.Find("Camera").transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y+1, player.position.z);
        rot = Quaternion.Euler(new Vector3(0, cam.eulerAngles.y, 0));
        transform.rotation = rot;
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Handle");
        // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리, 충돌할 레이어)
        bool isHit = Physics.SphereCast(transform.position, transform.transform.lossyScale.x / 2, transform.transform.forward, out hit, maxAimDistance, layerMask);

        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, transform.lossyScale.x / 2);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxAimDistance);
        }
    }
}
