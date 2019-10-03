using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MomiState
{
    Idle = 0,
    Move,
    Jump,
    Handle
}

public class MomiFSMManager : MonoBehaviour
{
    private bool isInit = false;
    public MomiState startState = MomiState.Idle;
    private Dictionary<MomiState, MomiFSMState> states = new Dictionary<MomiState, MomiFSMState>();

    private MomiState currentState;
    public MomiState CurrentState
    { get { return currentState; } }

    public MomiFSMState CurrentStateComponent
    { get { return states[currentState]; } }

    private void Awake()
    {
        MomiState[] stateValues = (MomiState[])System.Enum.GetValues(typeof(MomiState));
        foreach(MomiState s in stateValues)
        {
            System.Type FSMType = System.Type.GetType("Momi_" + s.ToString());
            MomiFSMState state = (MomiFSMState)GetComponent(FSMType);

            if (null == state)
                state = (MomiFSMState)gameObject.AddComponent(FSMType);

            states.Add(s, state);
            state.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(startState);
        isInit = true;
    }

    // Update is called once per frame
    // void Update() { }

    public void SetState(MomiState newState)
    {
        if (isInit)
        {
            states[currentState].enabled = false;
            states[currentState].EndState();
        }

        currentState = newState;
        states[currentState].BeginState();
        states[currentState].enabled = true;
    }
}
