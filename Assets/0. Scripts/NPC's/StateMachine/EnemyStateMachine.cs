using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentEnemyState { get; set; }

    public void Initialize(EnemyState startState)
    {
        currentEnemyState = startState;
        currentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentEnemyState.Exitstate();
        currentEnemyState = newState;
        currentEnemyState.EnterState();
    }
}
