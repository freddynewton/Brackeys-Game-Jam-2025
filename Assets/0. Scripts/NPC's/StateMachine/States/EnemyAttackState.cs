using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Animator _animator;
    private float _attackTime;
    private float _currentTime;

    public EnemyAttackState(ZombieInformation enemy, EnemyStateMachine StateMachine) : base(enemy, StateMachine)
    {
        _animator = enemyInformation.gameObject.GetComponentInChildren<Animator>();
        _attackTime = enemyInformation.GetAttackTime();
    }

    public override void EnterState()
    {
        base.EnterState();

        enemyInformation.TurnTowardsAttack();
        _animator.SetTrigger("Attack");
        _currentTime = _attackTime;
    }

    public override void Exitstate()
    {
        base.Exitstate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _currentTime -= Time.deltaTime;
        if (!(enemyInformation.IsAttackRange()) && (_currentTime <= 0))
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.chaseState);
        }
        else if (enemyInformation.IsAttackRange() && _currentTime <= 0)
        {
            enemyInformation.TurnTowardsAttack();
            _animator.SetTrigger("Attack");
            _currentTime = _attackTime;
        }
    }

}
