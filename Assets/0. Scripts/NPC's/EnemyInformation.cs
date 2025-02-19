using UnityEngine;

public class EnemyInformation : MonoBehaviour, IDamageable
{
    [Header("General Settings")]
    [SerializeField] private int _maxHp = 4;
    private int _currentHp;
    [SerializeField] private int _attackDamage { get; } = 1;
    [Range(0.1f, 10f)][SerializeField] float detectionRange { get; } = 5f;
    [Range(0.1f, 5f)][SerializeField] private float _attackRange { get; } = 2f;

    #region Idle Variables
    [Header("Idle Settings")]
    [Range(0f, 20f)] public float zombieShuffleRange = 4f;

    #endregion

    private EnemyStateMachine stateMachine;
    private EnemyIdleState idleState;
    private EnemyChaseState chaseState;
    private EnemyAttackState attackState;


    void Start()
    {
        _currentHp = _maxHp;
    }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();

        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {

    }
}
