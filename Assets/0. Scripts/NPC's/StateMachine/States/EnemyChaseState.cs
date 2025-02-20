using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform _playerTransform;
    private NPCNavigation _nPCNavigation;
    private float _waypointTime = 0.5f;
    private float _currentTime;

    public EnemyChaseState(EnemyInformation enemyInformation, EnemyStateMachine StateMachine) : base(enemyInformation, StateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _nPCNavigation = enemyInformation.gameObject.GetComponent<NPCNavigation>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _currentTime = _waypointTime;
    }

    public override void Exitstate()
    {
        base.Exitstate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            SetDestination();
            _currentTime = _waypointTime;
        }
        if (enemyInformation.IsAttackRange())
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.attackState);
        }

    }

    private void SetDestination()
    {
        if(_nPCNavigation != null)
        {
            _nPCNavigation.SetNewDestination(_playerTransform.position);
        }
    }
}
