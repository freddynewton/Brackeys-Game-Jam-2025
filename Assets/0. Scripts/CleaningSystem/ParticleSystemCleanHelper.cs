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

    private int _beginLayerMask;
    private int _endLayerMask;

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

        _beginLayerMask = LayerMask.NameToLayer("OnlyEnvironmentInteractable");
        _endLayerMask = LayerMask.NameToLayer("Environment");

        // Set the layer mask for the particle system and children
        SetLayerMask(particleSystems, _beginLayerMask);

        Invoke(nameof(SetEndLayerMask), 1f);

        foreach (var ps in particleSystems)
        {
            ps.Play();
        }
    }

    private void SetLayerMask(List<ParticleSystem> particleSystems, int layerMask)
    {
        // Set the layer mask for the particle system and children
        foreach (var ps in particleSystems)
        {
            ps.gameObject.layer = layerMask;
        }
    }

    private void SetEndLayerMask()
    {
        SetLayerMask(particleSystems, _endLayerMask);
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
