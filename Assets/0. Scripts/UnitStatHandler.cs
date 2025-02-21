using DG.Tweening;
using System;
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
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _hitVfx;

    [SerializeField] private GameObject _deathVfx;

    protected int _currentHp;

    public virtual void TakeDamage(int damage, Vector3 attackerTransformPosition)
    {
        StartFlickering();

        PlayHitVfx(attackerTransformPosition);

        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            Death();
        }
    }

    protected void PlayHitVfx(Vector3 attackerTransformPosition)
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

        _spriteRenderer ??= GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not assigned", gameObject);
        }
    }

    protected virtual void Death()
    {
        _spriteRenderer.DOKill();

        CreateDeathSprites();

        Destroy(gameObject);
    }

    protected void CreateDeathSprites()
    {
        foreach (Sprite sprite in _deathSpriteList)
        {
            GameObject deathSprite = new GameObject("DeathSprite");
            deathSprite.transform.position = transform.position;
            deathSprite.transform.localScale = transform.localScale;
            SpriteRenderer spriteRenderer = deathSprite.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 2;
            Rigidbody2D rb = deathSprite.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.linearDamping = 1.2f;
            rb.angularDamping = 1.2f;
            rb.mass = 2f;
            CircleCollider2D collider = deathSprite.AddComponent<CircleCollider2D>();
            collider.radius = 0.1f;

            rb.AddForce(new Vector2(UnityEngine.Random.Range(-3f, 3f) * 3f, UnityEngine.Random.Range(-3f, 3f)) * 3f, ForceMode2D.Impulse);

            Destroy(deathSprite, 300f);
        }
    }

    protected void StartFlickering()
    {
        _spriteRenderer.DOKill();

        // Using DoTween for sprite flickering
        _spriteRenderer.DOColor(_hitColor, _spriteFlashTime).OnComplete(() => _spriteRenderer.DOColor(Color.white, _spriteFlashTime)).SetLoops(_spriteFlashLoops);
    }
}
