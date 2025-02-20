using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class MeleeWeaponItem : WeaponItem
{
    [Header("Melee Weapon Item")]
    [SerializeField] private LayerMask _ignoreLayerMask;

    private List<IDamageable> _hittedDamageableCache = new();
    private float _cacheTime = 0.3f;
    private Collider2D _collider;

    public void ToggleCollider()
    {
        if (!_collider.enabled)
        {
            ClearHitCache();
        }

        _collider.enabled = !_collider.enabled;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (_hittedDamageableCache.Contains(damageable))
            {
                return;
            }

            damageable.TakeDamage(_damage, transform.position);

            _hittedDamageableCache.Add(damageable);

            StartCoroutine(DelayedRemoveOfCache(damageable));
        }
    }

    protected void ClearHitCache()
    {
        if (_hittedDamageableCache == null)
        {
            _hittedDamageableCache = new List<IDamageable>();
        }

        _hittedDamageableCache.Clear();
    }

  
    protected override void Awake()
    {
        base.Awake();

        _collider ??= GetComponent<Collider2D>();
        _collider.enabled = false;
        _collider.isTrigger = true;
    }

    private IEnumerator DelayedRemoveOfCache(IDamageable damageable)
    {
        yield return new WaitForSeconds(_cacheTime);

        _hittedDamageableCache.Remove(damageable);
    }
}
