using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : IState
{
    public StateMachine.StateIDs StateId => StateMachine.StateIDs.MAIN;

    public void Enter(StateMachine context)
    {
        Debug.Log("Entering MainState");
        CourseManager.Instance.StartMoving();
    }

    public void Update(StateMachine context) { }

    public void Exit(StateMachine context)
    {
        Debug.Log("Exiting MainState");
    }

    public StateMachine.StateIDs NextState()
    {
        return StateMachine.StateIDs.GAMEOVER;
    }

    public bool CheckTransition(StateMachine context)
    {
        // TODO 
        return false;
    }
}