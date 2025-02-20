using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private IDamageable _damageable;
    [SerializeField] private int _damage = 1;
    private bool _isInAnimation = false;

    private void Awake()
    {
        _damage = gameObject.GetComponentInParent<EnemyInformation>().GetEnemyDamagePerAttack();
        SetDamageable(GameObject.FindGameObjectWithTag("Player").GetComponent<UnitStatHandler>());
    }



    public void Hit()
    {
        _damageable.TakeDamage(_damage, transform.position);
    }

    public void SetDamageable(IDamageable damageable)
    {
        _damageable = damageable;
    }

    public void SetAndHit(IDamageable damageable)
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
