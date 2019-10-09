using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandle : MonoBehaviour
{
    public PuzzlePlate link_plate;
    public bool isCatch;

    //핸들을 잡을떄 포테이토를 넣어주기위해
    public GameObject[] potato;
    public GameObject potatoParent;
    public int[] plateNum;
    
    private void Start()
    {
        potatoParent = GameObject.Find("Potato");
        potato = GameObject.FindGameObjectsWithTag("Potato");
        transform.localRotation = link_plate.transform.localRotation;
    }
    private void Update()
    {
        // 핸들과 판이 같이 움직이도록
        if (!isCatch)
            transform.localRotation = link_plate.gameObject.transform.localRotation;
    }

    public void CatchCheck()
    {
        if (isCatch == false)
        {
            isCatch = true;
            link_plate.isRock = false;
            foreach (GameObject pot in potato)
            {
                for (int i = 0; i < plateNum.Length; i++)
                {
                    if (pot.GetComponent<Potato>().myNum == plateNum[i])
                    {
                        pot.transform.parent = link_plate.transform;
                    }
                }
            }
        }
        else
        {
            isCatch = false;
        }
    }
}
