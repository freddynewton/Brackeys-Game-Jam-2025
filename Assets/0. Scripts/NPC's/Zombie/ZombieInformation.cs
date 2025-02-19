using UnityEngine;

public class ZombieInformation : EnemyInformation
{
    private EnemyStateMachine stateMachine;
    private EnemyIdleState idleState;
    private EnemyChaseState chaseState;
    private EnemyAttackState attackState;

    #region Idle Variables
    [Header("Idle Settings")]
    [Range(0f, 5f)][SerializeField] float zombieShuffleRange { get; } = 3f;

    #endregion

    private void Awake()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }
}
