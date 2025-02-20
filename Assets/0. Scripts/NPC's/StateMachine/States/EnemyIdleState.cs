using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector2 _targetPos;
    private NPCNavigation _nPCNavigation;

    public EnemyIdleState(EnemyInformation enemyInformation, EnemyStateMachine stateMachine) : base(enemyInformation, stateMachine)
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

        if(_nPCNavigation != null && _nPCNavigation.HasStopped())
        {
            _targetPos = GetRandomPointInCircle();
            _nPCNavigation.SetNewDestination(_targetPos);
        }

        if (enemyInformation.IsAggroRange())
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.chaseState);
        }
    }

    private Vector2 GetRandomPointInCircle()
    {
        return enemyInformation.transform.position + (Vector3)(Random.insideUnitCircle * enemyInformation.zombieShuffleRange);
    }
}

