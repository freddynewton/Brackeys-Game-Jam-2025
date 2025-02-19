using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp = 10;

    public void TakeDamage(int damage)
    {
        _hp =- damage;
    }

    private void Death()
    {
        //it dies
    }
}
