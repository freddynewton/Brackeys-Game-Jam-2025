using UnityEngine;

public abstract class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] protected IDamageable _damageable;
    [SerializeField] protected Transform _damagePosition;
    [SerializeField] protected int _enemyDamage = 1;
    protected bool _isInAnimation = false;

    private void Awake()
    {
        GameObject playerTarget = GameObject.FindGameObjectWithTag("Player");
        _damagePosition = playerTarget.transform; 
        SetDamageable(playerTarget);

        _enemyDamage = gameObject.GetComponentInParent<ZombieInformation>().GetEnemyDamagePerAttack();
    }



    public void Hit()
    {
        _damageable.TakeDamage(_enemyDamage, transform.position);
    }

    public bool SetDamageable(GameObject target)
    {
        if (target == null) return false;

        foreach (var component in target.GetComponents<Component>())
        {
            if (component is IDamageable damageable)
            {
                _damageable = damageable;
                return true;
            }
        }

        return false;
    }

    public void SetAndHit(GameObject damageable)
    {
        SetDamageable(damageable);
        Hit();
    }

    public bool GetIsInAnimation()
    {
        return _isInAnimation;
    }
    public void SetIsInAnimation()
    {
        _isInAnimation = !_isInAnimation;
    }


}
