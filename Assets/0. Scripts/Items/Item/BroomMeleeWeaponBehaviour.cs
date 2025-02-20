using UnityEngine;

public class BroomMeleeWeaponBehaviour : MeleeWeaponItem
{
    public override void PrimaryInteract()
    {
        _animator.SetTrigger("Attack");
    }

    public override void SecondaryInteract()
    {
        Debug.Log("Secondary interact");
    }
}
