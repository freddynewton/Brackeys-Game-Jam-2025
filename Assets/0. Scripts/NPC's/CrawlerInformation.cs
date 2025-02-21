using UnityEngine;

public class CrawlerInformation : EnemyInformation
{
    [Header("Crawl Away Settings")]
    [Range(1f, 5f)][SerializeField] private float _moveAwayDistance = 2.5f;

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();

        idleState = new CrawlerIdleState(this, stateMachine);
        standState = new CrawlerStillState(this, stateMachine);
        moveAwayState = new CrawlerCrawlAwayState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    public override float GetMoveDistance()
    {
        return _moveAwayDistance;
    }


}
