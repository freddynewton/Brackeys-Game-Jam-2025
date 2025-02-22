using UnityEngine;

public class ZombieAnimationEvents : EnemyAnimationEvents
{
    [SerializeField] private GameObject _vFX;
    //[SerializeField] private Transform _damagePosition;

    public void DeployVFXOnTarget()
    {
        if (_damagePosition != null)
        {
            GameObject vfx = Instantiate(_vFX, _damagePosition);
        }
    }
}
