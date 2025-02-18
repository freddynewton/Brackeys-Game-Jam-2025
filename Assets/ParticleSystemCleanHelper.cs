using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            if (!ParticleCleanManager.Instance.IsParticleSystemRegistered(ps))
            {
                var triggerModule = ps.trigger;
                triggerModule.enabled = true;

                triggerModule.outside = ParticleSystemOverlapAction.Ignore;
                triggerModule.inside = ParticleSystemOverlapAction.Kill;
                triggerModule.enter = ParticleSystemOverlapAction.Ignore;
                triggerModule.exit = ParticleSystemOverlapAction.Ignore;

                // Register the particle system with the ParticleCleanManager
                ParticleCleanManager.Instance.RegisterParticleSystem(ps);
            }
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
