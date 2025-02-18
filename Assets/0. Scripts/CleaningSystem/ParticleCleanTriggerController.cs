using UnityEngine;

/// <summary>
/// Controls the particle clean trigger and manages its registration with the ParticleCleanManager.
/// </summary>
public class ParticleCleanTriggerController : MonoBehaviour
{
    #region private Fields

    /// <summary>
    /// The collider associated with this particle clean trigger.
    /// </summary>
    private Collider2D _collider;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the collider associated with this particle clean trigger.
    /// </summary>
    public Collider2D Collider => _collider;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Ensures the collider is set and registers the trigger with the ParticleCleanManager.
    /// </summary>
    private void Awake()
    {
        // Ensure the collider is set
        _collider ??= GetComponent<Collider2D>();

        // Register this trigger with the ParticleCleanManager if not already registered
        if (!ParticleCleanManager.Instance.IsParticleCleanTriggerRegistered(this))
        {
            ParticleCleanManager.Instance.RegisterParticleCleanTrigger(this);
        }
    }

    /// <summary>
    /// Called when the script instance is being destroyed.
    /// Unregisters the trigger from the ParticleCleanManager.
    /// </summary>
    private void OnDestroy()
    {
        // Unregister this trigger from the ParticleCleanManager if it is registered
        if (ParticleCleanManager.Instance.IsParticleCleanTriggerRegistered(this))
        {
            ParticleCleanManager.Instance.UnregisterParticleCleanTrigger(this);
        }
    }

    #endregion
}
