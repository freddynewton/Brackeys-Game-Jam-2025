using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private NPCNavigation _nPCNavigation;
    private float _waypointTime = 0.5f;
    private float _currentTime;

    public EnemyChaseState(ZombieInformation enemy, EnemyStateMachine StateMachine) : base(enemy, StateMachine)
    {
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
        if (enemyInformation.PlayerTransform == null)
        {
            Debug.Log("What the fuck");
            return;
        }

        base.FrameUpdate();

        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            SetDestination();
            _currentTime = _waypointTime;
        }
        if (enemyInformation.IsAttackRange())
        {
            _nPCNavigation.Stop();
            enemyInformation.stateMachine.ChangeState(enemyInformation.attackState);
        }

    }

    private void SetDestination()
    {
        if(_nPCNavigation != null)
        {
            _nPCNavigation.SetNewDestination(enemyInformation.PlayerTransform.position);
        }
    }
}
