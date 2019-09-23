using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongSlerpMove : MonoBehaviour
{
    public GameObject staff;
    Vector3 target;

    //다음패턴으로 이동
    enum PATTERN
    {
       FIRST_TARGET, STAFF_TARGET, END
    }
    PATTERN pattern;

    bool next;
    float time = 1;

    float a = 0.2f;

    void Awake()
    {
        staff =  GameObject.Find("Staff");
        pattern = PATTERN.END;
        next = false;
        time = 0;
        a = 0.2f;
    }
    
    void Update()
    {
        switch (pattern)
        {
            case PATTERN.FIRST_TARGET:
                transform.position = Vector3.Slerp(transform.position, target,Random.Range(0.05f,0.2f));
                if (next)
                {
                    Invoke("NextPattern", 0.5f);
                    next = false;
                }
                break;
            case PATTERN.STAFF_TARGET:
                transform.position = Vector3.Lerp(transform.position, staff.transform.GetChild(0).transform.position,Random.Range(0.05f, 0.2f) + a * Time.deltaTime );
                a += Time.deltaTime;
                time += Time.deltaTime;
                if (time > 2)
                {
                    NextPattern();
                    time = 0;
                }
                break;
            case PATTERN.END:
                gameObject.SetActive(false);
                break;
        }

    }
     //이펙트생성
    public void CreateEffet(Vector3 c_Position)
    {
        gameObject.SetActive(true);
        transform.position = c_Position;
        target = new Vector3(Random.Range(-3, 3) + c_Position.x,c_Position.y + 3, Random.Range(-3, 3) + c_Position.z);
        pattern = PATTERN.FIRST_TARGET;
        next = true;
        time = 0;
        a = 0;
    }
    //다음 단계로 이동
    void NextPattern()
    {
        next = true;
        switch (pattern)
        {
            case PATTERN.FIRST_TARGET:
                pattern = PATTERN.STAFF_TARGET;
                break;
            case PATTERN.STAFF_TARGET:
                pattern = PATTERN.END;
                gameObject.SetActive(false);
                staff.GetComponent<PlayerStaff>().kong++;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Staff_Crystal")
        {
            pattern = PATTERN.END;
            gameObject.SetActive(false);
            staff.GetComponent<PlayerStaff>().kong++;
        }
    }
}