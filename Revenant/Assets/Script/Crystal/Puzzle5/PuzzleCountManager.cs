//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PuzzleCountManager : MonoBehaviour
//{
//    public GameObject pattern1;
//    public GameObject pattern2;
//    public GameObject pattern3;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(pattern1.GetComponent<CrystalRotManager>().state == CrystalRotManager.DIRECTION_STATE.CLEAR)
//        {
//            if (pattern2.transform.position.y <= 1.3f)// (이부분 수정함 땃쥐야)
//                pattern2.transform.Translate(0, 2 * Time.deltaTime, 0);
//        }
//        if (pattern2.GetComponent<CrystalRotManager>().state == CrystalRotManager.DIRECTION_STATE.CLEAR)
//        {
//            if (pattern3.transform.position.y <= 1.3f)// (이부분 수정함 땃쥐야)
//                pattern3.transform.Translate(0, 2 * Time.deltaTime, 0);
//        }
//    }
//}
