using UnityEngine;

public class ZombieHP : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp = 4;

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        //it dies
    }
}
