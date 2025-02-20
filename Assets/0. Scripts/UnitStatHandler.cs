using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatHandler : MonoBehaviour, IDamageable
{
    [Header("Stats Settings")]
    [SerializeField] private int _maxHp = 4;

    [Header("Death Sprites")]
    [Tooltip("Sprites that will fly around after a thing gets destroyed")] 
    [SerializeField] private List<Sprite> _deathSpriteList = new();

    [Header("Hit Animation Effects")]
    [SerializeField] private float _freezeTime = 0.15f;
    [SerializeField] private float _spriteFlashTime = 0.1f;
    [SerializeField] private int _spriteFlashLoops = 2;
    [SerializeField] private Color _hitColor = Color.red;

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject _hitVfx;

    [SerializeField] private GameObject _deathVfx;

    private int _currentHp;

    public void TakeDamage(int damage, Vector3 attackerTransformPosition)
    {
        StartFlickering();

        PlayHitVfx(attackerTransformPosition);

        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            Death();
        }
    }

    private void PlayHitVfx(Vector3 attackerTransformPosition)
    {
        if (_hitVfx == null)
        {
            return;
        }

        // Instantiate hit VFX
        GameObject hitVfx = Instantiate(_hitVfx, transform.position, Quaternion.identity);

        // Set the rotation of the hit VFX to face the attacker, assume the current direction of the Vfx is right
        Vector3 direction = transform.position - attackerTransformPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hitVfx.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Awake()
    {
        _currentHp = _maxHp;

        spriteRenderer ??= GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not assigned", gameObject);
        }
    }


    private void Death()
    {
        spriteRenderer.DOKill();

        Destroy(gameObject);
    }

    private void StartFlickering()
    {
        spriteRenderer.DOKill();

        // Using DoTween for sprite flickering
        spriteRenderer.DOColor(_hitColor, _spriteFlashTime).OnComplete(() => spriteRenderer.DOColor(Color.white, _spriteFlashTime)).SetLoops(_spriteFlashLoops);
    }
}
