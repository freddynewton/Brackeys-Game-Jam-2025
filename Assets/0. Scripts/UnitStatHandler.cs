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
    private int _currentHp;

    private void Awake()
    {
        _currentHp = _maxHp;

        spriteRenderer ??= GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not assigned", gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Hit " + name);

        StartFlickering();

        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void StartFlickering()
    {
        spriteRenderer.DOKill();

        // Using DoTween for sprite flickering
        spriteRenderer.DOColor(_hitColor, _spriteFlashTime).OnComplete(() => spriteRenderer.DOColor(Color.white, _spriteFlashTime)).SetLoops(_spriteFlashLoops);
    }
}
