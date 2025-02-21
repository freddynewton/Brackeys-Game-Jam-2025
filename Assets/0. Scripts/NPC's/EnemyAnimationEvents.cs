using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private IDamageable _damageable;
    [SerializeField] private Transform _damagePosition;
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _vFX;
    private bool _isInAnimation = false;

    private void Awake()
    {
        _damage = gameObject.GetComponentInParent<EnemyInformation>().GetEnemyDamagePerAttack();
        if(_damagePosition == null)
        {
            _damagePosition = GameObject.FindGameObjectWithTag("Player").transform;
        }
        _damageable = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatHandler>();
    }



    public void Hit()
    {
        _damageable.TakeDamage(_damage, transform.position);
    }

    public void SetDamageable(GameObject damageable)
    {
        Component[] components = gameObject.GetComponents(typeof(Component));
        foreach (Component component in components)
        {
            if(component is IDamageable)
            {
                _damageable = (IDamageable)component;
                _damagePosition = component.transform;
            }
        }
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
            Instantiate(_vFX, _damagePosition);
        }
    }

}
