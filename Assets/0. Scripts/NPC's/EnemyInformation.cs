using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyInformation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _playerTransform;
    [SerializeField] protected UnitAnimator _unitAnimator;

    #region General Settings
    [Header("General Settings")]
    //[SerializeField] private int _attackDamage = 1;
    [Range(0.1f, 10f)][SerializeField] protected float _detectionRange = 4f;
    //[Range(0.1f, 5f)][SerializeField] private float _attackRange = 1.5f;
    //[Range(1f, 5f)][SerializeField] private float _timeBetweenAttack = 2.5f;
    [SerializeField] protected LayerMask _detectionMask;

    private bool _isInAnimation;
    #endregion

    #region Idle Variables
    [Header("Idle Settings")]
    [Range(0f, 20f)] public float zombieShuffleRange = 4f;
    [Range(0f, 20)] public float standStillTime = 4f;

    #endregion

    public EnemyStateMachine stateMachine { get; set; }
    public  EnemyState idleState { get; set; }
    public  EnemyState standState { get; set; }
    public  EnemyState chaseState { get; set; }
    public  EnemyState attackState { get; set; }
    public EnemyState moveAwayState { get; set; }

    public Transform PlayerTransform { get => _playerTransform; private set => _playerTransform = value; }

    private Door _door;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _door = FindFirstObjectByType<Door>();
        _unitAnimator = gameObject.GetComponentInChildren<UnitAnimator>();

        if (_playerTransform == null)
        {
            Console.WriteLine("Could not find Player or Player Tag");
        }
    }

    protected void FixedUpdate()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    #region Detection
    protected bool DetectionCast()
    {
        Collider2D collder = Physics2D.OverlapCircle(transform.position, _detectionRange, _detectionMask);
        
         if (collder.gameObject.tag == "Player")
         {
             return true;
         }
            
        
        return false;
    }
    private bool AttackCast()
    {
        if (_playerTransform == null)
        {
            return false;
        }

        RaycastHit2D ray = Physics2D.Raycast(transform.position, _playerTransform.position - transform.position, _detectionRange, _detectionMask);
        if(ray.collider != null)
        {
            return ray.collider.CompareTag("Player");
        }
        return false;
    }

    private float CheckDistance()
    {
        if (_playerTransform == null)
        {
            return 0;
        }

        return Vector2.Distance(transform.position, _playerTransform.position);
    }

    public bool IsAggroRange()
    {
        return DetectionCast();
    }

    public virtual bool IsAttackRange()
    {
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
    
    public virtual int GetEnemyDamagePerAttack()
    {
        return 0;
    }


    public virtual float GetAttackTime()
    {
        return 0f;
    }

    public virtual float GetMoveDistance()
    {
        return 0;
    }

    public void TurnTowardsAttack()
    {
        Vector2 direction = _playerTransform.position - this.transform.position;
        bool targetAtLeft = direction.x <= 0 ? true: false;
        if(targetAtLeft != _unitAnimator.IsLookingLeft)
        {
            _unitAnimator.TurnAround();
        }
    }

}
