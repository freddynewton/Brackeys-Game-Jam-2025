using System.Transactions;
using UnityEngine;

/// <summary>
/// Interface for objects that can take damage.
/// </summary>
public interface IDamageable
{
    public void TakeDamage(int damage, Vector3 attackerTransformPosition);
}
