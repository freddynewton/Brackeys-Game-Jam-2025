using UnityEngine;

public class CrawlerCrawlAwayState : EnemyState
{
    private Transform _playerTransform;
    private NPCNavigation _nPCNavigation;
    private float _waypointTime = 0.5f;
    private float _currentTime;

    public CrawlerCrawlAwayState(EnemyInformation enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _nPCNavigation = enemyInformation.gameObject.GetComponent<NPCNavigation>();
    }

    public override void EnterState()
    {
        base.EnterState();
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

        if(!enemyInformation.IsAggroRange())
        {
            enemyInformation.stateMachine.ChangeState(enemyInformation.idleState);
        }
    }

    private void SetDestination()
    {
        if (_nPCNavigation != null)
        {
            Vector2 OppesiteDirection = -(_playerTransform.position - enemyInformation.transform.position);

            _nPCNavigation.SetNewDestination(enemyInformation.transform.position + (Vector3)OppesiteDirection * enemyInformation.GetMoveDistance());
        }
    }
}
