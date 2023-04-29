using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : IState
{
    public StateMachine.StateIDs StateId => StateMachine.StateIDs.INTRO;

    public void Enter(StateMachine context)
    {
        Debug.Log("Entering IntroState");
    }

    public void Update(StateMachine context) { }

    public void Exit(StateMachine context)
    {
        Debug.Log("Exiting IntroState");
    }

    public StateMachine.StateIDs NextState()
    {
        return StateMachine.StateIDs.MAIN;
    }

    public bool CheckTransition(StateMachine context)
    {
        return true;
    }
}
