using UnityEngine;

public class BroomMeleeWeaponBehaviour : MeleeWeaponItem
{
    public override void PrimaryInteract()
    {
        _animator.SetTrigger("Attack");

        SoundManager.Instance.PlayBroomSwoosh();
    }

    public override void SecondaryInteract()
    {
        Debug.Log("Secondary interact");
    }
}
