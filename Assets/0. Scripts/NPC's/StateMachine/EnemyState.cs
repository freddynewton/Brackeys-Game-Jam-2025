using UnityEngine;

public class EnemyState
{
    protected EnemyInformation enemyInformation;
    protected EnemyStateMachine stateMachine;

    public EnemyState(EnemyInformation enemy, EnemyStateMachine stateMachine)
    {
        this.enemyInformation = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void Exitstate() { }
    public virtual void FrameUpdate() { }
}
