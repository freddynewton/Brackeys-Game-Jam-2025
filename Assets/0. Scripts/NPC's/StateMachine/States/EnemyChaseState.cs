using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform _playerTransform;
    private NPCNavigation _nPCNavigation;

    public EnemyChaseState(EnemyInformation enemyInformation, EnemyStateMachine StateMachine) : base(enemyInformation, StateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _nPCNavigation = enemyInformation.gameObject.GetComponent<NPCNavigation>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //_nPCNavigation = enemyInformation.gameObject.GetComponent<NPCNavigation>();
        if (_nPCNavigation != null)
        {
            _nPCNavigation.SetNewDestination(_playerTransform.position);
        }
    }

    public override void Exitstate()
    {
        base.Exitstate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (_nPCNavigation != null && _nPCNavigation.HasStopped())
        {
            _nPCNavigation.SetNewDestination(_playerTransform.position);
        }
        if (enemyInformation.IsAggroRange())
        {
            if (enemyInformation.IsAttackRange())
            {
                enemyInformation.stateMachine.ChangeState(enemyInformation.attackState);
            }
        }
        else
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.idleState);
        }
    }
}
