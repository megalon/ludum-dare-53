using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;

    private IState currentState;

    public IState CurrentState { get => currentState; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        ChangeState(StateIDs.INTRO);
    }

    public void ChangeState(StateIDs newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }

        switch (newState)
        {
            case StateIDs.INTRO:
                currentState = new IntroState();
                break;
            case StateIDs.MAIN:
                currentState = new MainState();
                break;
            case StateIDs.GAMEOVER:
                currentState = new GameOverState();
                break;
            default:
                Debug.LogError("Unhandled state: " + newState);
                break;
        }

        currentState.Enter(this);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this);

            if (currentState.CheckTransition(this))
            {
                ChangeState(currentState.NextState());
            }
        }
    }

    public enum StateIDs
    {
        INTRO,
        MAIN,
        GAMEOVER
    }
}
