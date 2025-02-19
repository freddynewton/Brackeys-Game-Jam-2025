using System;
using UnityEngine;

public class ZombieAttackAnimation
{
    private Animator _animator;

    public ZombieAttackAnimation(Animator animator)
    {
        _animator = animator;

    }
    
    public void ActivateAttackanimation()
    {
        _animator.SetTrigger("Attack");
    }
}
