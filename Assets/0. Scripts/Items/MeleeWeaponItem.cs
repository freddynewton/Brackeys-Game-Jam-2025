using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class MeleeWeaponItem : WeaponItem
{
    protected Collider2D _collider;

    private List<IDamageable> _hittedDamageableCache = new();
    private float _cacheTime = 0.3f;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (_hittedDamageableCache.Contains(damageable))
            {
                return;
            }

            damageable.TakeDamage(_damage);

            _hittedDamageableCache.Add(damageable);

            StartCoroutine(DelayedRemoveOfCache(damageable));
        }
    }

    protected void ClearHitCache()
    {
        _hittedDamageableCache.Clear();
    }

    private IEnumerator DelayedRemoveOfCache(IDamageable damageable)
    {
        yield return new WaitForSeconds(_cacheTime);

        _hittedDamageableCache.Remove(damageable);
    }

    protected override void Awake()
    {
        base.Awake();

        _collider ??= GetComponent<Collider2D>();
        _collider.enabled = false;
    }
}
