using UnityEngine;

public abstract class WeaponItem : BaseItem
{
    [Header("Weapon Item")]
    [SerializeField] protected int _damage;

    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator ??= GetComponent<Animator>();
    }
}
