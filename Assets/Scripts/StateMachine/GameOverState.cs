using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : IState
{
    public StateMachine.StateIDs StateId => StateMachine.StateIDs.GAMEOVER;

    public void Enter(StateMachine context)
    {
        Debug.Log("Entering GameOverState");
    }

    public void Update(StateMachine context) { }

    public void Exit(StateMachine context)
    {
        Debug.Log("Exiting GameOverState");
    }

    public StateMachine.StateIDs NextState()
    {
        return StateMachine.StateIDs.INTRO;
    }

    public bool CheckTransition(StateMachine context)
    {
        return true;
    }
}
