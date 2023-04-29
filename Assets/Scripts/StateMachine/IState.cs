public interface IState
{
    StateMachine.StateIDs StateId { get; }

    void Enter(StateMachine context);
    void Update(StateMachine context);
    bool CheckTransition(StateMachine context);
    void Exit(StateMachine context);
    StateMachine.StateIDs NextState();
}