using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the registration and unregistration of particle systems and their associated clean triggers.
/// </summary>
public class ParticleCleanManager : Singleton<ParticleCleanManager>
{
    #region Fields

    /// <summary>
    /// List of registered particle clean triggers.
    /// </summary>
    private List<ParticleCleanTriggerController> _particleCleanTriggersList = new();

    /// <summary>
    /// List of registered particle systems.
    /// </summary>
    private List<ParticleSystem> _particleSystemsList = new();

    #endregion

    #region Public Methods

    /// <summary>
    /// Checks if a particle system is registered.
    /// </summary>
    /// <param name="particleSystem">The particle system to check.</param>
    /// <returns>True if the particle system is registered, otherwise false.</returns>
    public bool IsParticleSystemRegistered(ParticleSystem particleSystem)
    {
        return _particleSystemsList.Contains(particleSystem);
    }

    /// <summary>
    /// Registers a particle system and adds all registered triggers to it.
    /// </summary>
    /// <param name="particleSystem">The particle system to register.</param>
    public void RegisterParticleSystem(ParticleSystem particleSystem)
    {
        _particleSystemsList.Add(particleSystem);

        foreach (ParticleCleanTriggerController particleCleanTrigger in _particleCleanTriggersList)
        {
            particleSystem.trigger.AddCollider(particleCleanTrigger.Collider);
        }
    }

    /// <summary>
    /// Unregisters a particle system and cleans its collider triggers.
    /// </summary>
    /// <param name="particleSystem">The particle system to unregister.</param>
    public void UnRegisterParticleSystem(ParticleSystem particleSystem)
    {
        CleanColliderTrigger(particleSystem);
        _particleSystemsList.Remove(particleSystem);
    }

    /// <summary>
    /// Registers a particle clean trigger and adds it to all registered particle systems.
    /// </summary>
    /// <param name="particleCleanTrigger">The particle clean trigger to register.</param>
    public void RegisterParticleCleanTrigger(ParticleCleanTriggerController particleCleanTrigger)
    {
        _particleCleanTriggersList.Add(particleCleanTrigger);

        foreach (ParticleSystem particleSystem in _particleSystemsList)
        {
            particleSystem.trigger.AddCollider(particleCleanTrigger.GetComponent<Collider>());
        }
    }

    /// <summary>
    /// Unregisters a particle clean trigger.
    /// </summary>
    /// <param name="particleCleanTrigger">The particle clean trigger to unregister.</param>
    public void UnregisterParticleCleanTrigger(ParticleCleanTriggerController particleCleanTrigger)
    {
        _particleCleanTriggersList.Remove(particleCleanTrigger);
    }

    /// <summary>
    /// Checks if a particle clean trigger is registered.
    /// </summary>
    /// <param name="particleCleanTrigger">The particle clean trigger to check.</param>
    /// <returns>True if the particle clean trigger is registered, otherwise false.</returns>
    public bool IsParticleCleanTriggerRegistered(ParticleCleanTriggerController particleCleanTrigger)
    {
        return _particleCleanTriggersList.Contains(particleCleanTrigger);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Cleans the collider triggers of a particle system by removing null colliders.
    /// </summary>
    /// <param name="particleSystem">The particle system to clean.</param>
    private void CleanColliderTrigger(ParticleSystem particleSystem)
    {
        for (int i = 0; i < particleSystem.trigger.colliderCount; i++)
        {
            var component = particleSystem.trigger.GetCollider(i);
            if (component == null)
            {
                particleSystem.trigger.RemoveCollider(i);
            }
        }
    }

    #endregion
}
