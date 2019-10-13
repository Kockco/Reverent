using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDustEffect : MonoBehaviour
{
    MomiFSMManager manager;
    ParticleSystem particle;
    public GameObject stepDust;

    bool isPlay;

    void Start()
    {
        stepDust.transform.position = transform.position;
        manager = GameObject.Find("Momi").GetComponent<MomiFSMManager>();
        particle = stepDust.gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        stepDust.transform.position = transform.position;

        if (manager.CurrentState == MomiState.Jump)
        {
            isPlay = false;
            particle.Stop();
        }
        else if (manager.CurrentState != MomiState.Idle && isPlay == false) // || manager.CurrentState == MomiState.Move)
        {
            isPlay = true;
            particle.Play();
        }
    }
}
