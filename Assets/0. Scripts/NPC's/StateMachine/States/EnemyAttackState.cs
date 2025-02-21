using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Animator _animator;
    private float _attackTime;
    private float _currentTime;

    public EnemyAttackState(EnemyInformation enemyInformation, EnemyStateMachine StateMachine) : base(enemyInformation, StateMachine)
    {
        _animator = enemyInformation.gameObject.GetComponentInChildren<Animator>();
        _attackTime = enemyInformation.GetAttackTime();
    }

    public override void EnterState()
    {
        base.EnterState();


        _animator.SetTrigger("Attack");
        _currentTime = _attackTime;

        SoundManager.Instance.PlayNormalGrowl(_animator.gameObject);
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
            _animator.SetTrigger("Attack");
            _currentTime = _attackTime;
            Debug.Log("bite attack 2");
        }
    }
}
