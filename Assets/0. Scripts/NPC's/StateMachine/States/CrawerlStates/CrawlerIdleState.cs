using UnityEngine;

public class CrawlerIdleState : EnemyState
{
    private Vector2 _targetPos;
    private NPCNavigation _nPCNavigation;

    public CrawlerIdleState(EnemyInformation enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
        _nPCNavigation = enemyInformation.gameObject.GetComponent<NPCNavigation>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetPos = GetRandomPointInCircle();
        //_nPCNavigation = enemyInformation.gameObject.GetComponent<NPCNavigation>();
        if (_nPCNavigation != null)
        {
            _nPCNavigation.SetNewDestination(_targetPos);
        }
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

        if (_nPCNavigation != null && _nPCNavigation.HasStopped())
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.standState);
        }
    }

    private Vector2 GetRandomPointInCircle()
    {
        return enemyInformation.transform.position + (Vector3)(Random.insideUnitCircle * enemyInformation.zombieShuffleRange);
    }
}
