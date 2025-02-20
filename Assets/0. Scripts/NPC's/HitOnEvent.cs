using UnityEngine;

public class HitOnEvent : MonoBehaviour
{
    [SerializeField] private IDamageable _damageable;
    [SerializeField] private int _damage = 1;



    public void Hit()
    {
        _damageable.TakeDamage(_damage, transform.position);
    }

    public void SetDamageable(IDamageable damageable)
    {
        _damageable = damageable;
    }
}
