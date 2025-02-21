using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private IDamageable _damageable;
    [SerializeField] private Transform _damagePosition;
    [SerializeField] private int _enemyDamage = 1;
    [SerializeField] private GameObject _vFX;
    private bool _isInAnimation = false;

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

    public void DeployVFXOnTarget()
    {
        if(_damagePosition != null)
        {
            GameObject vfx = Instantiate(_vFX, _damagePosition);
        }
    }

}
