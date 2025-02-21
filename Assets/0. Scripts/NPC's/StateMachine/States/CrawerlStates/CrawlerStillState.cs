using UnityEngine;

public class CrawlerStillState : EnemyState
{
    private float _waitTime;

    public CrawlerStillState(CrawlerInformation enemyInformation, EnemyStateMachine stateMachine) : base(enemyInformation, stateMachine)
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

        if (enemyInformation.IsAggroRange())
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.moveAwayState);
        }

        _waitTime -= Time.deltaTime;
        if (_waitTime <= 0)
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.idleState);
        }
    }
}

