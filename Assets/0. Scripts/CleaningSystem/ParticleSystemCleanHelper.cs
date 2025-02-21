using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Helper class to manage the registration and unregistration of particle systems with the ParticleCleanManager.
/// </summary>
public class ParticleSystemCleanHelper : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// List of particle systems found in the children and itself.
    /// </summary>
    private List<ParticleSystem> particleSystems = new();

    #endregion

    #region Unity Methods

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Registers all particle systems in the children and itself with the ParticleCleanManager.
    /// </summary>
    private void Awake()
    {
        // Get all particle systems in children and itself with Linq
        particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();

        // Add all particle systems to the ParticleCleanManager
        foreach (var ps in particleSystems)
        {
            var triggerModule = ps.trigger;
            triggerModule.enabled = true;

            triggerModule.outside = ParticleSystemOverlapAction.Ignore;
            triggerModule.inside = ParticleSystemOverlapAction.Kill;
            triggerModule.enter = ParticleSystemOverlapAction.Ignore;
            triggerModule.exit = ParticleSystemOverlapAction.Ignore;
        }

        // Set the collision quality to High initially
        SetCollisionQuality(particleSystems, ParticleSystemCollisionQuality.Medium);

        Invoke(nameof(SetEndCollisionQuality), 1f);

        foreach (var ps in particleSystems)
        {
            ps.Play();
        }
    }

    private void SetCollisionQuality(List<ParticleSystem> particleSystems, ParticleSystemCollisionQuality quality)
    {
        // Set the collision quality for the particle system and children
        foreach (var ps in particleSystems)
        {
            var collisionModule = ps.collision;
            collisionModule.quality = quality;
        }
    }

    private void SetEndCollisionQuality()
    {
        SetCollisionQuality(particleSystems, ParticleSystemCollisionQuality.High);

        // Add all particle systems to the ParticleCleanManager
        foreach (var ps in particleSystems)
        {
            // Register the particle system with the ParticleCleanManager
            ParticleCleanManager.Instance.RegisterParticleSystem(ps);
        }
    }

    /// <summary>
    /// Called when the script instance is being destroyed.
    /// Unregisters all particle systems from the ParticleCleanManager.
    /// </summary>
    private void OnDestroy()
    {
        // Remove all particle systems from the ParticleCleanManager
        foreach (var ps in particleSystems)
        {
            if (ParticleCleanManager.Instance.IsParticleSystemRegistered(ps))
            {
                ParticleCleanManager.Instance.UnRegisterParticleSystem(ps);
            }
        }
    }

    #endregion
}
