using UnityEngine;

public class EnemyStandState : EnemyState
{
    private float _waitTime;

    public EnemyStandState(EnemyInformation enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _waitTime = enemyInformation.standStillTime;
    }

    public override void Exitstate()
    {
        base.Exitstate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _waitTime =- Time.deltaTime;
        Debug.Log(_waitTime);
        if (_waitTime <= 0)
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.idleState);
        }
    }
}
