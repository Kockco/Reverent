using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MomiFSMManager))]
public class MomiFSMState : MonoBehaviour
{
    protected MomiFSMManager manager;
    protected GameObject aim;
    protected Animator anime;

    [SerializeField] protected float moveSpeed = 3;

    void Awake()
    {
        manager = GetComponent<MomiFSMManager>();
        anime = GetComponent<Animator>();
        aim = GameObject.Find("Aim");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public virtual void BeginState()
    {
    }

    public virtual void EndState()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void OnTriggerStay(Collider col)
    {
        
    }
}
