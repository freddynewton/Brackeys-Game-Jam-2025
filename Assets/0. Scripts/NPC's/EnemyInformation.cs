using System;
using UnityEngine;

public class EnemyInformation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerPosition;

    #region General Settings
    [Header("General Settings")]
    [SerializeField] private int _attackDamage = 1;
    [Range(0.1f, 10f)][SerializeField] private float _detectionRange = 4f;
    [Range(0.1f, 5f)][SerializeField] private float _attackRange = 1.5f;
    [Range(1f, 5f)][SerializeField] private float _timeBetweenAttack = 2.5f;
    [SerializeField] private LayerMask _detectionMask;

    private bool _isInAnimation;
    #endregion

    #region Idle Variables
    [Header("Idle Settings")]
    [Range(0f, 20f)] public float zombieShuffleRange = 4f;
    [Range(0f, 20)] public float standStillTime = 4f;

    #endregion

    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyStandState standState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }


    void Start()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;

        if (_playerPosition == null)
        {
            Console.WriteLine("Could not find Player or Player Tag");
        }
    }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();

        idleState = new EnemyIdleState(this, stateMachine);
        standState = new EnemyStandState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    private void FixedUpdate()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    #region Detection
    private bool DetectionCast()
    {
        Collider2D[] collderArray = Physics2D.OverlapCircleAll(transform.position, _detectionRange, _detectionMask);
        foreach(Collider2D collider2D in collderArray)
        {
            if (collider2D.gameObject.tag == "Player")
            {
                return true;
            }
            
        }
        return false;
    }
    private bool AttackCast()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, _playerPosition.position - transform.position, _detectionRange, _detectionMask);
        if(ray.collider != null)
        {
            return ray.collider.CompareTag("Player");
        }
        return false;
    }

    private float CheckDistance()
    {
        return Vector2.Distance(transform.position, _playerPosition.position);
    }

    public bool IsAggroRange()
    {
        if (DetectionCast())
        {
            return true;
        }
        return false;
    }

    public bool IsAttackRange()
    {
        if (CheckDistance() <= _attackRange && AttackCast())
        {
            return true;
        }
        return false;
    }
    #endregion

    public bool GetIsInAnimation()
    {
        return _isInAnimation;
    }
    public void SetIsInAnimation(bool animation)
    {
         _isInAnimation = animation;
    }

    public int GetEnemyDamagePerAttack()
    {
        return _attackDamage;
    }

    public float GetAttackTime()
    {
        return _timeBetweenAttack;
    }
}
