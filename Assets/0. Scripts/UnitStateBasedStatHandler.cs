using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateBasedStatHandler : UnitStatHandler
{
    [Header("State Based Settings")]
    [SerializeField] private List<StatState> _states = new List<StatState>();

    protected override void Death()
    {
    }

    public override void TakeDamage(int damage, Vector3 attackerTransformPosition)
    {
        if (_currentHp <= 0)
        {
            return;
        }

        StartFlickering();

        PlayVfx(_hitVfx, attackerTransformPosition);

        _currentHp -= 1;

        if (_states.Exists(state => state.Health == _currentHp))
        {
            UpdateSprite();
        }

        SoundManager.Instance.PlayHitSound();
    }

    private void UpdateSprite()
    {
        // Get Sprite based on current health based on the Health property of the StatState using Linq
        Sprite sprite = _states.Find(state => state.Health == _currentHp).Sprite;

        if (sprite != null)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}

[Serializable]
public class StatState
{
    public int Health;

    public Sprite Sprite;
}
