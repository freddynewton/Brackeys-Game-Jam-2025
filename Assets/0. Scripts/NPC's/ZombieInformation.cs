using UnityEngine;

public class ZombieInformation : EnemyInformation
{
    [Header("Attack Settings")]
    [SerializeField] private int _attackDamage = 1;
    [Range(0.1f, 5f)][SerializeField] private float _attackRange = 1.5f;
    [Range(1f, 5f)][SerializeField] private float _timeBetweenAttack = 2.5f;

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();

        idleState = new EnemyIdleState(this, stateMachine);
        standState = new EnemyStandState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    protected bool AttackCast()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, PlayerTransform.position - transform.position, _detectionRange, _detectionMask);
        if (ray.collider != null)
        {
            return ray.collider.CompareTag("Player");
        }
        return false;
    }

    protected float CheckDistance()
    {
        return Vector2.Distance(transform.position, PlayerTransform.position);
    }

    public override bool IsAttackRange()
    {
        if (CheckDistance() <= _attackRange && AttackCast())
        {
            return true;
        }
        return false;
    }

    public override int GetEnemyDamagePerAttack()
    {
        return _attackDamage;
    }

    public override float GetAttackTime()
    {
        return _timeBetweenAttack;
    }
}
